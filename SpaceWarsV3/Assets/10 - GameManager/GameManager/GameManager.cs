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
    [SerializeField] private AudioSource audioSource;

    private float volume = 0.5f;

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

        uiCntrl.ResetAttributes(levelData);

        GameObject fighter = Instantiate(levelData.Fighter, new Vector3(), Quaternion.identity);
        fighter.GetComponent<FighterCntrl>().SetLevel(levelData, clickingPlane);
        fighter.SetActive(true);
        fighter.GetComponent<FighterCntrl>().StartEngage();

        gameCamera.GetComponent<CameraCntrl>().StartEngagement(fighter.transform);

        uiCntrl.RenderBattlePanel();

        enemyManager.StartEngagement(fighter, levelData);

        spaceCntrl.StartEnvironment();
    }

    /**
     * OnFighterHit() - If the fighter has been hit, the remaining health 
     * needs to be displayed via the UI.
     */
    private void OnFighterHit(float remainingDamage)
    {
        uiCntrl.UpdateHealthBar(remainingDamage);
    }

    /**
     * OnPlayerLooses() - Starts the process if a fighter has been 
     * destroyed.  A "You Loose" banner is displayed across the screen
     */
    private void OnPlayerLooses()
    {
        uiCntrl.BattleBanner("You Loose", this);

        spaceCntrl.EndEnvironment();
    }

    /**
     * OnDisengage() - 
     */
    public void OnDisengage()
    {
        uiCntrl.BattleBanner("Disengage", this);

        EventManager.Instance.InvokeOnDisengage();

        spaceCntrl.EndEnvironment();
    }

    /**
     * OnPlayerWins() - Start the process if a player wins and all
     * enemies have been destroyed.  The "Congratulations" banner is displayed
     * across the screne.
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

    public void ShowSettingsMenu()
    {
        uiCntrl.RenderSettingPanel();
    }

    public void ShowSettingMenuDone()
    {
        uiCntrl.RenderMainMenu();
    }

    public void VolumeUp()
    {
        volume += 0.1f;
        if (volume > 1.0f) volume = 1.0f;
        audioSource.volume = volume;
        uiCntrl.SetVolume(volume);
    }

    public void VolumeDown()
    {
        volume -= 0.1f;
        if (volume < 0.0f) volume = 0.0f;
        audioSource.volume = volume;
        uiCntrl.SetVolume(volume);
    }

    private void OnEnable()
    {
        EventManager.Instance.OnStartBattle     += StartBattle;
        EventManager.Instance.OnPlayerWins      += OnPlayerWins;
        EventManager.Instance.OnFighterHit      += OnFighterHit;
        EventManager.Instance.OnPlayerLooses    += OnPlayerLooses;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnStartBattle     -= StartBattle;
        EventManager.Instance.OnPlayerWins      -= OnPlayerWins;
        EventManager.Instance.OnFighterHit      -= OnFighterHit;
        EventManager.Instance.OnPlayerLooses    -= OnPlayerLooses;
    }
}

public interface NewGameIf
{
    public void ReStartNewGame();
}