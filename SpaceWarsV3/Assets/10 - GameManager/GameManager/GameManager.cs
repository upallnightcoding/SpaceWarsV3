using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour, NewGameIf
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private UICntrl uiCntrl;
    [SerializeField] private GameObject gameCamera;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private Transform clickingPlane;
    [SerializeField] private CreateSpaceCntrl spaceCntrl;

    private float health = 100.0f;
    private float maxHealth = 100.0f;

    public void Start()
    {
        StartMainMenu();
    }

    /***********************************/
    /*** Main Menu Selection Options ***/
    /***********************************/

    public void NewGameAction()
    {
        EventManager.Instance.InvokeOnNewGameAction();
    }

    public void LoadGameBtn()
    {

    }

    public void SettingsBtn()
    {

    }

    public void ScoreBoardBtn()
    {

    }

    public void QuitBtn()
    {

    }

    public void StartMainMenu()
    {
        uiCntrl.RenderMainMenu();
    }

    /*********************************/
    /*** Select Level Menu Options ***/
    /*********************************/

    /**
     * StartEngagement() - Starts the engagement play between the player and 
     * enemy ships.  The depth of the engagement is determined by the 
     * "levelData" attribute that contains all the data driven information.
     */
    public void StartBattle(LevelData levelData)
    {
        // Return the current weapons selected during inventory
        //-----------------------------------------------------
        levelData.Ammo = uiCntrl.ammoListCntrl.GetCurrentWeapon();
        levelData.Missile = uiCntrl.missileListCntrl.GetCurrentWeapon();
        levelData.Shield = uiCntrl.shieldListCntrl.GetCurrentWeapon();

        GameObject fighter = Instantiate(levelData.Fighter, new Vector3(), Quaternion.identity);
        fighter.GetComponent<FighterCntrl>().SetLevel(levelData, clickingPlane);
        fighter.SetActive(true);
        fighter.GetComponent<FighterCntrl>().StartEngage();

        gameCamera.GetComponent<CameraCntrl>().StartEngagement(fighter.transform);

        uiCntrl.RenderBattlePanel();

        enemyManager.StartEngagement(fighter, levelData);

        spaceCntrl.StartEnvironment();
    }

    public void SaveGameState()
    {
        SaveLoadManager slm = new SaveLoadManager();
        slm.type = LevelType.HAVOC;
        string json = JsonUtility.ToJson(slm);
        Debug.Log($"Save Game State: {json}");
        File.WriteAllText(Application.dataPath + "/save.txt", json);

        string loaded = File.ReadAllText(Application.dataPath + "/save.txt");
        SaveLoadManager s = JsonUtility.FromJson<SaveLoadManager>(loaded);
        Debug.Log($"FromJson: {s.type}");
    }

    /**
     * OnFighterHit() - 
     */
    private void OnFighterHit(float remainingDamage)
    {
        uiCntrl.UpdateHealthBar(remainingDamage, maxHealth);
    }

    /**
     * OnPlayerLooses() - Starts the process if a fighter has been 
     * destroyed.
     */
    private void OnPlayerLooses()
    {
        uiCntrl.BattleBanner("You Loose", this);

        spaceCntrl.EndEnvironment();
    }

    /**
     * OnPlayerWins() -
     */
    private void OnPlayerWins()
    {
        uiCntrl.BattleBanner("Congratulations", this);

        spaceCntrl.EndEnvironment();
    }

    /**
     * ReStartNewGame() - When a new game is purposed, all existing ships must
     * be destroyed, the camer is put back in the correct position and then a
     * new game can be started.
     */
    public void ReStartNewGame()
    {
        EventManager.Instance.InvokeDestroyAllShips();

        gameCamera.GetComponent<CameraCntrl>().PositionCameraAtIdle();

        NewGameAction();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnStartBattle += StartBattle;
        EventManager.Instance.OnPlayerWins += OnPlayerWins;

        EventManager.Instance.OnFighterHit += OnFighterHit;
        EventManager.Instance.OnPlayerLooses += OnPlayerLooses;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnStartBattle -= StartBattle;
        EventManager.Instance.OnPlayerWins -= OnPlayerWins;
        EventManager.Instance.OnFighterHit -= OnFighterHit;
        EventManager.Instance.OnPlayerLooses -= OnPlayerLooses;
    }
}

public interface NewGameIf
{
    public void ReStartNewGame();
}

