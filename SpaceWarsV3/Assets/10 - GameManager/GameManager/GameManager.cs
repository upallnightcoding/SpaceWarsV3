using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private FighterSO fighter1;

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

    public void StartEngagement()
    {
        Debug.Log("Start Engagement ...");
    }

    public void SetLevelTutorial()
    {
        levelData = new LevelData(LevelType.TUTORIAL);
    }

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
