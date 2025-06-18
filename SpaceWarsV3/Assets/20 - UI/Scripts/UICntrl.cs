using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICntrl : MonoBehaviour
{
    [SerializeField] public GameDataSO gameData;

    [Header("Menu Starting Objects ...")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject selectLevel;
    [SerializeField] private GameObject selectShip;
    [SerializeField] private GameObject engagementButton;
    [SerializeField] private GameObject inventoryDone;
    [SerializeField] private GameObject settingDone;

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
    [SerializeField] private Slider shieldsBar;
    [SerializeField] private TMP_Text shieldsBarText;
    [SerializeField] private Slider missileBar;
    [SerializeField] private TMP_Text missileBarText;

    [Header("UI Panels ...")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject fighterSelectionPanel;
    [SerializeField] private GameObject selectLevelPanel;
    [SerializeField] private GameObject inventorySelectionPanel;
    [SerializeField] private GameObject engagementPanel;
    [SerializeField] private GameObject battlePanel;
    [SerializeField] private GameObject settingPanel;

    [Header("Settings ...")]
    [SerializeField] private TMP_Text volumeText;
    [SerializeField] private TMP_Text attackText;

    [Header("Inventory List ...")]
    [SerializeField] public WeaponsSelectionListCntrl ammoListCntrl;
    [SerializeField] public WeaponsSelectionListCntrl missileListCntrl;
    [SerializeField] public WeaponsSelectionListCntrl shieldListCntrl;

    [Header("Battle Attributes ...")]
    [SerializeField] private GameObject banner;
    [SerializeField] private TMP_Text bannerMessage;
    [SerializeField] private TMP_Text gameLevel;
    [SerializeField] private TMP_Text enemyCount;

    [Header("Mini Radar ...")]
    [SerializeField] private GameObject enemyRadar;
    [SerializeField] private Transform radarComponent;

    [Header("Enemy Controls ...")]
    [SerializeField] private TMP_Text havocEnemyCount;

    private WeaponSO[] ammoList;
    private WeaponSO[] missileList;
    private WeaponSO[] shieldList;

    private Dictionary<int, RectTransform> miniRadar = null;

    private int gamePlayLevel = 1;

    public void Start()
    {
        ammoList = gameData.ammoList;
        missileList = gameData.missileList;
        shieldList = gameData.shieldList;

        miniRadar = new Dictionary<int, RectTransform>();

        SetWeaponItems();
        UpdateHealthBar(100.0f);
        UpdateShieldBar(0.0f, 0.0f);
        UpdateAmmoBar(0, 0);
    }

    public void SetLowEnemyAttack()
    {
        attackText.text = "Low";
    }

    public void SetMedEnemyAttack()
    {
        attackText.text = "Med";
    }

    public void SetHghEnemyAttack()
    {
        attackText.text = "Hgh";
    }

    public void HavocEnemyCountAdd()
    {
        int count = int.Parse(havocEnemyCount.text);
        if (count < 20)
        {
            havocEnemyCount.text = (++count).ToString();
            EventManager.Instance.InvokeOnHavocEnemyUpdate(count);
        }
    }

    public void HavocEnemyCountSub()
    {
        int count = int.Parse(havocEnemyCount.text);
        if (count > 1)
        {
            havocEnemyCount.text = (--count).ToString();
            EventManager.Instance.InvokeOnHavocEnemyUpdate(count);
        }
    }

    public void OnRadarUpdate(bool action, int enemy, Vector3 position)
    {
        if (action)
        {
            float x = (float)((80.0f * position.x) / 500.0f);
            float y = (float)((80.0f * position.z) / 500.0f);

            if (miniRadar.TryGetValue(enemy, out RectTransform radarElement))
            {
                radarElement.transform.localPosition = new Vector3(x, y, 0.0f);
            } else
            {
                GameObject element = Instantiate(enemyRadar, radarComponent);
                miniRadar.Add(enemy, element.GetComponent<RectTransform>());

                element.transform.localPosition = new Vector3(x, y, 0.0f);
            }
        } else
        {
            if (miniRadar.TryGetValue(enemy, out RectTransform radarElement))
            {
                radarElement.gameObject.SetActive(false);
                miniRadar.Remove(enemy);
            }
        }
    }

    public void ResetAttributes(LevelData levelData)
    {
        UpdateHealthBar(100.0f);

        float shieldDur = levelData.Shield.totalDurationSec;
        UpdateShieldBar(shieldDur, shieldDur);

        int maxAmmoCount = levelData.Ammo.maxRounds;
        UpdateAmmoBar(maxAmmoCount, maxAmmoCount);

        int maxMissiles = levelData.Missile.maxMissiles;
        UpdateMissileBar(maxMissiles, maxMissiles);
    }

    /** 
     * RenderMainMenu() - Render the Main Menu, close any other menu before
     * displaying main menu.
     */
    public void RenderMainMenu()
    {
        CloseAllPanels();
        mainMenuPanel.SetActive(true);

        if (EventSystem.current)
        {
            EventSystem.current.SetSelectedGameObject(mainMenu);
        }
    }

    /**
     * SetVolume() - Sets the text volumn display.  This attribute is only
     * available on the settings menu.
     */
    public void SetVolume(float volume)
    {
        volumeText.text = $"{volume:F1}";
    }

    /**
     * BattleBanner() - Display a banner across the screen informing the 
     * player of the current status.  The banner will only display for
     * a limited amount of time.
     */
    public void BattleBanner(string message, NewGameIf callback)
    {
        StartCoroutine(DisplayBattleBanner(message, callback));
    }

    /**
     * DisplayBattleBanner() - 
     */
    private IEnumerator DisplayBattleBanner(string message, NewGameIf callback)
    {
        bannerMessage.text = message;
        banner.SetActive(true);

        yield return new WaitForSeconds(3.0f);

        banner.SetActive(false);

        callback.ReStartNewGame();
    }

    /**
     * RenderSelectLevelPanel() -
     */
    public void RenderSelectLevelPanel()
    {
        CloseAllPanels();
        mainMenuCntrl.StopFighterDisplay();
        gameLevel.text = $"Level ({gamePlayLevel})";
        selectLevelPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(selectLevel);
    }

    public void RenderSettingPanel()
    {
        CloseAllPanels();
        settingPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(settingDone);
    }

    public void SetGameLevel(int value)
    {
        gamePlayLevel = value;
    }

    public void RenderSelectFighterPanel()
    {
        CloseAllPanels();
        fighterSelectionPanel.SetActive(true);
        fighterSelectionCntrl.NewGameAction();

        EventSystem.current.SetSelectedGameObject(selectShip);
    }

    public void RenderSelectInventoryPanel()
    {
        CloseAllPanels();
        inventorySelectionPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(inventoryDone);
    }

    /**
     * RenderEngage() - 
     */
    public void RenderEngage()
    {
        CloseAllPanels();
        engagementPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(engagementButton);
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

    public void UpdateMissileBar(int count, int maxMissiles)
    {
        float percent = count / (float) maxMissiles;
        missileBar.value = percent;
        missileBarText.text = $"{(100.0f * missileBar.value):F0}%";
    }

    public void UpdateShieldBar(float shield, float maxShield)
    {
        int percent = 0;

        if (maxShield > 0.0f)
        {
            shieldsBar.value = shield / maxShield;
            percent = (int)(100.0f * shieldsBar.value);
        } 

        shieldsBarText.text = $"{percent}%";
    }

    /**
     * UpdateHealthBar() - Update health bar as the fighter is attached
     * by the enemy.  Updates are done by events as missiles trigger
     * the fighter collision box.
     */
    public void UpdateHealthBar(float health)
    {
        healthBar.value = health / 100.0f;
        int percent = (int)(100.0f * healthBar.value);
        healthBarText.text = $"{percent:F0}%";
    }

    /**
     * UpdateAmmoBar() - Update the ammo bar each time the player fires
     * a missile.  When the player has run out of ammo, execute the 
     * reload state.
     */
    public void UpdateAmmoBar(int ammoCount, int maxAmmoCount)
    {
        float percentage = 0.0f;

        if (maxAmmoCount > 0)
        {
            percentage = (float)ammoCount / maxAmmoCount;
        }

        ammoBar.image.color = Color.green;
        ammoBar.value = percentage;
        ammoBarText.text = $"{(int)(percentage * 100.0f):F0}%";
    }

    /**
     * UpdateReload() -
     */
    public void UpdateReload(float timing, float reloadTime)
    {
        ammoBar.image.color = Color.yellow;
        ammoBar.value = timing / reloadTime;
        ammoBarText.text = "";
    }

    /************************************/
    /*** Fighter Level Menu Selection ***/
    /************************************/

    /**
     * SetWeaponItems() - 
     */
    public void SetWeaponItems()
    {
        ammoListCntrl.SetItem(ammoList);
        missileListCntrl.SetItem(missileList);
        shieldListCntrl.SetItem(shieldList);
    }

    /**
     * CloseAllPanels() - Close all panels, this is usually done before 
     * displaying a new panel to the user.  Order of closing the 
     * panels does not matter.
     */
    private void CloseAllPanels()
    {
        mainMenuPanel.SetActive(false);
        fighterSelectionPanel.SetActive(false);
        selectLevelPanel.SetActive(false);
        inventorySelectionPanel.SetActive(false);
        engagementPanel.SetActive(false);
        battlePanel.SetActive(false);
        settingPanel.SetActive(false);

        if (EventSystem.current)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    /**
     * UpdateEnemyCount() - 
     */
    private void UpdateEnemyCount(int value)
    {
        enemyCount.text = value.ToString();
    }

    /**
     * OnEnable() - 
     */
    private void OnEnable()
    {
        EventManager.Instance.OnUpdateAmmoBar += UpdateAmmoBar;
        EventManager.Instance.OnUpdateReload += UpdateReload;
        EventManager.Instance.OnUpdateShield += UpdateShieldBar;
        EventManager.Instance.OnSetEnemyCount += UpdateEnemyCount;
        EventManager.Instance.OnUpdateMissile += UpdateMissileBar;
        EventManager.Instance.OnRadarUpdate += OnRadarUpdate;
    }

    /**
     * OnDisable() - 
     */
    private void OnDisable()
    {
        EventManager.Instance.OnUpdateAmmoBar -= UpdateAmmoBar;
        EventManager.Instance.OnUpdateReload -= UpdateReload;
        EventManager.Instance.OnUpdateShield -= UpdateShieldBar;
        EventManager.Instance.OnSetEnemyCount -= UpdateEnemyCount;
        EventManager.Instance.OnUpdateMissile -= UpdateMissileBar;
        EventManager.Instance.OnRadarUpdate -= OnRadarUpdate;
    }
}
