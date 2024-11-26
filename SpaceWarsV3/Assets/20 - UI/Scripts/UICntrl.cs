using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICntrl : MonoBehaviour
{
    [Header("UI Controllers ...")]
    [SerializeField] private FighterSelectionCntrl fighterSelectionCntrl;
    [SerializeField] private UIAnimationCntrl uIAnimationCntrl;
    [SerializeField] private MainMenuCntrl mainMenuCntrl;
    [SerializeField] private EngagementCntrl engagementCntrl;

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

    [Header("UI Attributes ...")]
    [SerializeField] private TMP_Text currentCoins;

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
        //RenderPanel(PanelType.SELECT_LEVEL_PANEL);
    }

    public void RenderMainMenu()
    {
        mainMenuPanel.SetActive(true);
    }

    /**
     * RenderSelectLevelPanel() -
     */
    public void RenderSelectLevelPanel()
    {
        CloseAllPanels();
        mainMenuCntrl.StopFighterDisplay();
        selectLevelPanel.SetActive(true);
    }

    public void RenderSelectFighterPanel()
    {
        CloseAllPanels();
        fighterSelectionPanel.SetActive(true);
        fighterSelectionCntrl.NewGameAction();
    }

    public void RenderSelectInventoryPanel()
    {
        CloseAllPanels();
        inventorySelectionPanel.SetActive(true);
    }

    public void RenderEngage()
    {
        CloseAllPanels();
        engagementPanel.SetActive(true);
    }

    public void StartEngagementCountDown(LevelData levelData)
    {
        engagementCntrl.StartEngagementCountDown(levelData);
    }

    /**
     * StartEngagement() - Update the UI when it is time for an engagement
     * to begin.  
     */
    //public void StartEngagement()
    //{
      //  battlePanel.SetActive(true);
    //}

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

    public void SetWeaponItems()
    {
        ammoListCntrl.SetItem(ammoList);
        missileListCntrl.SetItem(missileList);
        shieldListCntrl.SetItem(shieldList);
    }

    private void CloseAllPanels()
    {
        mainMenuPanel.SetActive(false);
        fighterSelectionPanel.SetActive(false);
        selectLevelPanel.SetActive(false);
        inventorySelectionPanel.SetActive(false);
        engagementPanel.SetActive(false);
        battlePanel.SetActive(false);
    }

    private void OnEnable()
    {
        //EventManager.Instance.OnStartBattle += StartEngagement;
        EventManager.Instance.OnUpdateAmmoBar += UpdateAmmoBar;
        EventManager.Instance.OnUpdateReload += UpdateReload;
    }

    private void OnDisable()
    {
        //EventManager.Instance.OnStartBattle -= StartEngagement;
        EventManager.Instance.OnUpdateAmmoBar -= UpdateAmmoBar;
        EventManager.Instance.OnUpdateReload -= UpdateReload;
    }

    /*private enum PanelType
    {
        MAIN_MENU_PANEL,
        FIGHTER_SELECTION_PANEL,
        SELECT_LEVEL_PANEL,
        INVENTORY_SELECTION_PANEL,
        ENGAGEMENT_PANEL,
        BATTLE_PANEL
    }*/
}
