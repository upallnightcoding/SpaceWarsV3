using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    [SerializeField] private UICntrl uiCntrl;

    //private NewGameState state = NewGameState.IDLE;

    private LevelData levelData = null;

    public void NewGameAction()
    {
        uiCntrl.RenderSelectLevelPanel();
    }

    public void StartTutorialLevel()
    {
        levelData = new LevelData(LevelType.TUTORIAL);

        uiCntrl.RenderSelectFighterPanel();
    }

    public void StartHavocLevel()
    {
        levelData = new LevelData(LevelType.HAVOC);

        uiCntrl.RenderSelectFighterPanel();
    }

    public void StartBerserkLevel()
    {
        levelData = new LevelData(LevelType.BERSERK);

        uiCntrl.RenderSelectFighterPanel();
    }

    public void StartGamePlayLevel()
    {
        levelData = new LevelData(LevelType.LEVEL);

        uiCntrl.RenderSelectFighterPanel();
    }

    public void SelectFighter()
    {
        uiCntrl.RenderSelectFighterPanel();
    }

    public void SelectInventory()
    {
        uiCntrl.RenderSelectInventoryPanel();
    }

    private void OnPlayerWins()
    {
        uiCntrl.RaiseGamePlayLevel();
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

    public void StartEngagmentCountDown()
    {
        uiCntrl.StartEngagementCountDown(levelData);
    }

    public void SaveState()
    {
        SaveLoadManager slm = levelData.SaveState();
        string json = JsonUtility.ToJson(slm);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public void LoadState()
    {
        string loaded = File.ReadAllText(Application.dataPath + "/save.txt");
        SaveLoadManager s = JsonUtility.FromJson<SaveLoadManager>(loaded);
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
