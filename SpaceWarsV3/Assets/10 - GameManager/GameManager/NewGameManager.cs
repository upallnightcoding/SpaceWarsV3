using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    const string SAVE_FILE = "CyberFighter.dat";

    [SerializeField] private UICntrl uiCntrl;

    private LevelData levelData = new LevelData();

    public void NewGameAction()
    {
        uiCntrl.RenderSelectLevelPanel();
    }

    public void StartTutorialLevel()
    {
        levelData.SetType(LevelType.TUTORIAL);

        uiCntrl.RenderSelectFighterPanel();

        ResetAttributes();
    }

    public void StartHavocLevel()
    {
        levelData.SetType(LevelType.HAVOC);

        uiCntrl.RenderSelectFighterPanel();

        ResetAttributes();
    }

    public void StartBerserkLevel()
    {
        levelData.SetType(LevelType.BERSERK);

        uiCntrl.RenderSelectFighterPanel();

        ResetAttributes();
    }

    public void StartGamePlayLevel()
    {
        levelData.SetType(LevelType.LEVEL);

        uiCntrl.RenderSelectFighterPanel();

        ResetAttributes();
    }

    public void SelectFighter()
    {
        uiCntrl.RenderSelectFighterPanel();
    }

    private void ResetAttributes()
    {
        uiCntrl.UpdateHealthBar(100.0f);
    }

    private void OnPlayerWins()
    {
        uiCntrl.SetGameLevel(levelData.RaiseGamePlayLevel());
    }

    /**
     * DoneInventorySelection() - When the inventory selection is done.  This
     * function will switch over the display to the Engagement Panel and 
     * capture the selected inventory.
     */
    public void DoneInventorySelection()
    {
        uiCntrl.RenderEngage();
    }

    /**
     * StartEngagmentCountDown() - When the engage button is selected, this 
     * option will start the count down to actual engagement.
     */
    public void StartEngagmentCountDown()
    {
        uiCntrl.StartEngagementCountDown(levelData);
    }

    public void SaveState()
    {
        SaveLoadObject slm = levelData.SaveState();
        string json = JsonUtility.ToJson(slm);
        File.WriteAllText(Application.dataPath + "/" + SAVE_FILE, json);
    }

    public void LoadState()
    {
        string loaded = File.ReadAllText(Application.dataPath + "/" + SAVE_FILE);
        SaveLoadObject slm = JsonUtility.FromJson<SaveLoadObject>(loaded);
        levelData.SetType(LevelType.UNKNOWN);
        levelData.LoadState(slm, uiCntrl);

        uiCntrl.GetComponent<MainMenuCntrl>().StopFighterDisplay();

        //uiCntrl.RenderSelectInventoryPanel();
        uiCntrl.RenderSelectLevelPanel();
    }

    public void FighterSelection(GameObject selectedFighter)
    {
        levelData.Fighter = selectedFighter;

        uiCntrl.RenderSelectInventoryPanel();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnNewGameAction += NewGameAction;
        EventManager.Instance.OnFighterSelection += FighterSelection;
        EventManager.Instance.OnPlayerWins += OnPlayerWins;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnNewGameAction -= NewGameAction;
        EventManager.Instance.OnFighterSelection -= FighterSelection;
        EventManager.Instance.OnPlayerWins -= OnPlayerWins;
    }
}
