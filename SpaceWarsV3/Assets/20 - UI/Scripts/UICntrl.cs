using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICntrl : MonoBehaviour
{
    [SerializeField] private FighterSelectionCntrl fighterSelectionCntrl;
    [SerializeField] private UIAnimationCntrl uIAnimationCntrl;
    [SerializeField] private MainMenuCntrl mainMenuCntrl;

    [SerializeField] private TMP_Text currentCoins;

    [Header("Health & Status Bar ...")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text healthBarText;
    [SerializeField] private Slider ammoBar;
    [SerializeField] private TMP_Text ammoBarText;

    [Header("UI Panels ...")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject fighterSelectionPanel;
    [SerializeField] private GameObject selectLevelPanel;
    [SerializeField] private GameObject inventorySelectionPanel;
    [SerializeField] private GameObject engagementPanel;
    [SerializeField] private GameObject battlePanel;

    [Header("Inventory List ...")]
    [SerializeField] public WeaponListCntrl ammoListCntrl;
    [SerializeField] public WeaponListCntrl missileListCntrl;
    [SerializeField] public WeaponListCntrl shieldListCntrl;

    [Header("Weapon List ...")]
    [SerializeField] private WeaponSO[] ammoList;
    [SerializeField] private WeaponSO[] missileList;
    [SerializeField] private WeaponSO[] shieldList;

    public void Start()
    {
        SetWeaponItems();
        UpdateHealthBar(1.0f);
        UpdateAmmoBar(0, 1);
    }

    /****************************/
    /*** Main Menu Selections ***/
    /****************************/

    /**
     * NewGameAction() - When the user selects "New Game" from the main menu.
     */
    public void NewGameAction()
    {
        RenderPanel(PanelType.SELECT_LEVEL_PANEL);
    }

    /**
     * StartEngagement() - Update the UI when it is time for an engagement
     * to begin.  
     */
    public void StartEngagement()
    {
        RenderPanel(PanelType.BATTLE_PANEL);
    }

    /****************************/
    /*** Health & Status Bars ***/
    /****************************/

    public void UpdateHealthBar(float value)
    {
        healthBar.value = value;
    }

    public void UpdateAmmoBar(int ammoCount, int maxAmmoCount)
    {
        ammoBar.value = (float)ammoCount / (float)maxAmmoCount;
        ammoBarText.text = $"{ammoCount}/{maxAmmoCount}";
    }

    public void UpdateReload(float timing, float reloadTime)
    {
        ammoBar.value = timing / reloadTime;
        ammoBarText.text = "";
    }

    /************************************/
    /*** Fighter Level Menu Selection ***/
    /************************************/

    public void SetLevelTutorial()
    {
        RenderPanel(PanelType.FIGHTER_SELECTION_PANEL);

        EventManager.Instance.InvokeOnSetLevelTutorial();
    }

    public void SetLevelHavoc()
    {
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

    public void FighterSelection(GameObject selectedFighter)
    {
        RenderPanel(PanelType.INVENTORY_SELECTION_PANEL);
    }

    //public void BattlePanel()
    //{
      //  RenderPanel(PanelType.BATTLE_PANEL);
    //}

    /**
     * DoneInventorySelection() - When the inventory selection is done.  This
     * function will switch over the display to the Engagement Panel and 
     * capture the selected inventory.
     */
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
                mainMenuCntrl.StopFighterDisplay();
                selectLevelPanel.SetActive(true);
                break;
            case PanelType.INVENTORY_SELECTION_PANEL:
                inventorySelectionPanel.SetActive(true);
                break;
            case PanelType.ENGAGEMENT_PANEL:
                engagementPanel.SetActive(true);
                break;
            case PanelType.BATTLE_PANEL:
                battlePanel.SetActive(true);
                break;
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.OnNewGameAction += NewGameAction;
        EventManager.Instance.OnFighterSelection += FighterSelection;
        EventManager.Instance.OnStartEngagement += StartEngagement;
        EventManager.Instance.OnUpdateAmmoBar += UpdateAmmoBar;
        EventManager.Instance.OnUpdateReload += UpdateReload;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnNewGameAction -= NewGameAction;
        EventManager.Instance.OnFighterSelection -= FighterSelection;
        EventManager.Instance.OnStartEngagement -= StartEngagement;
        EventManager.Instance.OnUpdateAmmoBar -= UpdateAmmoBar;
        EventManager.Instance.OnUpdateReload -= UpdateReload;
    }

    private enum PanelType
    {
        MAIN_MENU_PANEL,
        FIGHTER_SELECTION_PANEL,
        SELECT_LEVEL_PANEL,
        INVENTORY_SELECTION_PANEL,
        ENGAGEMENT_PANEL,
        BATTLE_PANEL
    }
}
