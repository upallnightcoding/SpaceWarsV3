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
    [SerializeField] private BattlePanelCntrl battlePanelCntrl;

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

    [Header("Battle Attributes ...")]
    [SerializeField] private GameObject banner;
    [SerializeField] private TMP_Text bannerMessage;

    public void Start()
    {
        SetWeaponItems();
        UpdateHealthBar(100.0f, 100.0f);
        //UpdateAmmoBar(1, 1);
    }

    /****************************/
    /*** Main Menu Selections ***/
    /****************************/

    /**
     * NewGameAction() - When the user selects "New Game" from the main menu.
     */
    /*public void NewGameAction()
    {
        //RenderPanel(PanelType.SELECT_LEVEL_PANEL);
    }*/

    public void RenderMainMenu()
    {
        mainMenuPanel.SetActive(true);
    }

    public void BattleBanner(string message, GameManagerIf callback)
    {
        StartCoroutine(DisplayBattleBanner(message, callback));
    }

    private IEnumerator DisplayBattleBanner(string message, GameManagerIf callback)
    {
        bannerMessage.text = message;
        banner.SetActive(true);

        yield return new WaitForSeconds(3.0f);

        banner.SetActive(false);

        callback.EndBattleCallback();
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

    public void RenderBattlePanel()
    {
        CloseAllPanels();
        battlePanel.SetActive(true);
        battlePanelCntrl.StartTimer();
    }

    public void StartEngagementCountDown(LevelData levelData)
    {
        engagementCntrl.StartEngagementCountDown(levelData);
    }

    /****************************/
    /*** Health & Status Bars ***/
    /****************************/

    /**
     * UpdateHealthBar() - Update health bar as the fighter is attached
     * by the enemy.  Updates are done by events as missiles trigger
     * the fighter collision box.
     */
    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthBar.value = health / maxHealth;
        int percent = (int)(100.0f * healthBar.value);
        healthBarText.text = $"{percent}%";
    }

    /**
     * UpdateAmmoBar() - Update the ammo bar each time the player fires
     * a missile.  When the player has run out of ammo, execute the 
     * reload state.
     */
    public void UpdateAmmoBar(int ammoCount, int maxAmmoCount)
    {
        ammoBar.image.color = Color.green;
        ammoBar.value = (float)ammoCount / maxAmmoCount;
        ammoBarText.text = $"{ammoCount}/{maxAmmoCount}";
    }

    public void UpdateReload(float timing, float reloadTime)
    {
        ammoBar.image.color = Color.yellow;
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
        EventManager.Instance.OnUpdateAmmoBar += UpdateAmmoBar;
        EventManager.Instance.OnUpdateReload += UpdateReload;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnUpdateAmmoBar -= UpdateAmmoBar;
        EventManager.Instance.OnUpdateReload -= UpdateReload;
    }
}
