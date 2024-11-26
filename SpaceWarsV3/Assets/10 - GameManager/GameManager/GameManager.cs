using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private UICntrl uiCntrl;
    [SerializeField] private GameObject gameCamera;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private NewGameManager newGameManager;

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

        enemyManager.StartEngagement(fighter, levelData);
    }

    /**
     * QuitEngagment() - 
     */
    public void QuitEngagment()
    {
        uiCntrl.NewGameAction();

        gameCamera.GetComponent<CameraCntrl>().PositionCameraAtIdle();

        EventManager.Instance.InvokeOnDestoryRequest();
    }

    /**
     * FighterSelection() - 
     */
   

    private void OnEnable()
    {
        EventManager.Instance.OnStartBattle += StartBattle;
        EventManager.Instance.OnQuitEngagment += QuitEngagment;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnStartBattle -= StartBattle;
        EventManager.Instance.OnQuitEngagment -= QuitEngagment;
    }
}
