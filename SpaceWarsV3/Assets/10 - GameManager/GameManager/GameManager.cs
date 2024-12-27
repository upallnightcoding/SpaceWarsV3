using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, GameManagerIf
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private UICntrl uiCntrl;
    [SerializeField] private GameObject gameCamera;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private NewGameManager newGameManager;

    private float health = 100.0f;
    private float maxHealth = 100.0f;

    public void Start()
    {
        uiCntrl.RenderMainMenu();
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
        levelData.Shild = uiCntrl.shieldListCntrl.GetCurrentWeapon();

        GameObject fighter = Instantiate(levelData.Fighter, new Vector3(), Quaternion.identity);
        fighter.GetComponent<FighterCntrl>().SetLevel(levelData);
        fighter.SetActive(true);
        fighter.GetComponent<FighterCntrl>().StartEngage();

        gameCamera.GetComponent<CameraCntrl>().StartEngagement(fighter.transform);

        uiCntrl.RenderBattlePanel();

        enemyManager.StartEngagement(fighter, levelData, 3);
    }

    /**
     * EndBattle() - Ends the battle engagement by displaying a banner 
     * and setting up the diplay to allow for the selection of the next
     * level.  The banner is displayed for a set amount of time and the
     * remaining logic is trigger via a callback interface.
     */
    public void EndBattle(string message)
    {
        uiCntrl.BattleBanner(message, this);
    }

    /**
     * EndBattleCallback() - 
     */
    public void EndBattleCallback()
    {
        gameCamera.GetComponent<CameraCntrl>().PositionCameraAtIdle();

        //EventManager.Instance.InvokeOnDestoryRequest();

        NewGameAction();
    }

    private void OnFighterHit(float remainingDamage)
    {
        uiCntrl.UpdateHealthBar(remainingDamage, maxHealth);
    }

    /**
     * OnDestroyFighter() - Starts the process if a fighter has been 
     * destroyed.
     */
    private void OnDestroyFighter()
    {
        EndBattle("You Loose");
    }

    private void OnPlayerWins()
    {
        EndBattle("Congratulations");
    }

    private void OnEnable()
    {
        EventManager.Instance.OnStartBattle += StartBattle;
        EventManager.Instance.OnPlayerWins += OnPlayerWins;

        EventManager.Instance.OnFighterHit += OnFighterHit;
        EventManager.Instance.OnDestroyFighter += OnDestroyFighter;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnStartBattle -= StartBattle;
        EventManager.Instance.OnPlayerWins -= OnPlayerWins;
        EventManager.Instance.OnFighterHit -= OnFighterHit;
        EventManager.Instance.OnDestroyFighter -= OnDestroyFighter;
    }
}

public interface GameManagerIf
{
    public void EndBattleCallback();
}
