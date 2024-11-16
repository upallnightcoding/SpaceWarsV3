using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private FighterSO fighter1;
    [SerializeField] private UICntrl uiCntrl;

    [SerializeField] private GameObject gameCamera;

    [SerializeField] private EnemyManager enemyManager;

    private LevelData levelData = null;

    public void Start()
    {
        //fighter1.Create(new Vector3(2.6f, 0.0f, 7.7f));

        //fighter1.Create(gameData.displayFighterCenter);
        //NewGameAction();
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
    public void StartEngagement()
    {
        // Return the current weapons selected during inventory
        //-----------------------------------------------------
        levelData.Ammo = uiCntrl.ammoListCntrl.GetCurrentWeapon();
        levelData.Missile = uiCntrl.missileListCntrl.GetCurrentWeapon();
        levelData.Shild = uiCntrl.shieldListCntrl.GetCurrentWeapon();

        GameObject fighter = Instantiate(levelData.Fighter, new Vector3(), Quaternion.identity);
        fighter.GetComponent<FighterCntrl>().SetLevel(levelData);
        fighter.SetActive(true);

        StartCoroutine(WaitBeforeStarting(fighter));

        LeanTween.moveLocal(gameCamera, new Vector3(0.0f, 150.0f, 0.0f), 2.0f);
        LeanTween.rotateLocal(gameCamera, new Vector3(90.0f, 0.0f, 0.0f), 2.0f);

        gameCamera.GetComponent<CameraCntrl>().StartEngagement(fighter.transform);

        enemyManager.StartEngagement(fighter, levelData);
    }

    /**
     * WaitBeforeStarting() - This is an additional wait period before an
     * engagement starts.  This allows the figther to sit in the middle of
     * the screen before the first enemy emerges.
     */
    private IEnumerator WaitBeforeStarting(GameObject go)
    {
        yield return new WaitForSeconds(3.0f);
        go.GetComponent<FighterCntrl>().StartEngage();
    }

    /**
     * SetLevelTutorial() -
     */
    public void SetLevelTutorial()
    {
        levelData = new LevelData(LevelType.TUTORIAL);
    }

    /**
     * FighterSelection() - 
     */
    public void FighterSelection(GameObject selectedFighter)
    {
        levelData.Fighter = selectedFighter;
    }

    private void OnEnable()
    {
        EventManager.Instance.OnFighterSelection += FighterSelection;
        EventManager.Instance.OnSetLevelTutorial += SetLevelTutorial;
        EventManager.Instance.OnStartEngagement += StartEngagement;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnFighterSelection -= FighterSelection;
        EventManager.Instance.OnSetLevelTutorial -= SetLevelTutorial;
        EventManager.Instance.OnStartEngagement -= StartEngagement;
    }
}
