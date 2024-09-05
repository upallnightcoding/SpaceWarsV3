using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICntrl : MonoBehaviour
{
    [SerializeField] FighterSelectionCntrl fighterSelectionCntrl;

    [Header("UI Panels ...")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject fighterSelectionPanel;

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

    public void SetWeaponItems()
    {
        ammoListCntrl.SetItem(ammoList);
        missileListCntrl.SetItem(missileList);
        shieldListCntrl.SetItem(shieldList);
    }

    public void NewGameAction()
    {
        CloseAllPanels();
        fighterSelectionPanel.SetActive(true);

        fighterSelectionCntrl.NewGameAction();
    }

    private void FighterSelection()
    {

    }

    private void CloseAllPanels()
    {
        mainMenuPanel.SetActive(false);
        fighterSelectionPanel.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.Instance.OnNewGameAction -= NewGameAction;
    }

    private void OnEnable()
    {
        EventManager.Instance.OnNewGameAction += NewGameAction;
    }
}
