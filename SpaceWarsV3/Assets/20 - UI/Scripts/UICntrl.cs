using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICntrl : MonoBehaviour
{
    [SerializeField] private FighterSelectionCntrl fighterSelectionCntrl;
    [SerializeField] private UIAnimationCntrl uIAnimationCntrl;

    [SerializeField] private TMP_Text currentCoins;

    [Header("UI Panels ...")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject fighterSelectionPanel;
    [SerializeField] private GameObject selectLevelPanel;
    [SerializeField] private GameObject inventorySelectionPanel;
    [SerializeField] private GameObject engagementPanel;

    [Header("Inventory List ...")]
    [SerializeField] private WeaponListCntrl ammoListCntrl;
    [SerializeField] private WeaponListCntrl missileListCntrl;
    [SerializeField] private WeaponListCntrl shieldListCntrl;

    [Header("Weapon List ...")]
    [SerializeField] private WeaponSO[] ammoList;
    [SerializeField] private WeaponSO[] missileList;
    [SerializeField] private WeaponSO[] shieldList;

    public void Start()
    {
        SetWeaponItems();
    }

    public void DoneInventorySelection()
    {
        RenderPanel(PanelType.ENGAGEMENT_PANEL);
    }

    public void SetWeaponItems()
    {
        ammoListCntrl.SetItem(ammoList);
        missileListCntrl.SetItem(missileList);
        shieldListCntrl.SetItem(shieldList);
    }

    public void NewGameAction()
    {
        RenderPanel(PanelType.SELECT_LEVEL_PANEL);
    }

    public void SetLevelTutorial()
    {
        Debug.Log("Set Level Tutorial ...");
        RenderPanel(PanelType.FIGHTER_SELECTION_PANEL);
    }

    public void SetLevelHavoc()
    {
        Debug.Log("Set Level Havoc ...");
        RenderPanel(PanelType.FIGHTER_SELECTION_PANEL);
    }

    public void SetLevelTooEasy()
    {
        Debug.Log("Set Level Too Easy ...");
        RenderPanel(PanelType.FIGHTER_SELECTION_PANEL);
    }

    public void SetLevelLevels()
    {
        Debug.Log("Set Level Levels ...");
        RenderPanel(PanelType.FIGHTER_SELECTION_PANEL);
    }

    public void FighterSelection(GameObject fighter)
    {
        RenderPanel(PanelType.INVENTORY_SELECTION_PANEL);
    }

    private void RenderPanel(PanelType panel)
    {
        mainMenuPanel.SetActive(false);
        fighterSelectionPanel.SetActive(false);
        selectLevelPanel.SetActive(false);
        inventorySelectionPanel.SetActive(false);
        engagementPanel.SetActive(false);

        switch (panel)
        {
            case PanelType.FIGHTER_SELECTION_PANEL:
                fighterSelectionPanel.SetActive(true);
                fighterSelectionCntrl.NewGameAction();
                break;
            case PanelType.SELECT_LEVEL_PANEL:
                selectLevelPanel.SetActive(true);
                break;
            case PanelType.INVENTORY_SELECTION_PANEL:
                inventorySelectionPanel.SetActive(true);
                break;
            case PanelType.ENGAGEMENT_PANEL:
                engagementPanel.SetActive(true);
                break;
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.OnNewGameAction += NewGameAction;
        EventManager.Instance.OnFighterSelection += FighterSelection;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnNewGameAction -= NewGameAction;
        EventManager.Instance.OnFighterSelection -= FighterSelection;
    }

    private enum PanelType
    {
        MAIN_MENU_PANEL,
        FIGHTER_SELECTION_PANEL,
        SELECT_LEVEL_PANEL,
        INVENTORY_SELECTION_PANEL,
        ENGAGEMENT_PANEL
    }
}
