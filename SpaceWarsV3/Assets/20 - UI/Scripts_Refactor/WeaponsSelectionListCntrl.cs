using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSelectionListCntrl : MonoBehaviour
{
    [SerializeField] private WeaponsButtonCntrl[] weaponListItem;
    [SerializeField] private InventorySelectionCntrl inventorySelectionCntrl;

    private int currentWeapon = -1;

    private int nWeapons = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Return the current selected weapon
    public WeaponSO GetCurrentWeapon() => weaponListItem[currentWeapon].GetWeapon();

    public void SelectedButton(int index)
    {
        if (currentWeapon != -1)
        {
            weaponListItem[currentWeapon].ToggleSelection();
        }

        currentWeapon = index;
        weaponListItem[currentWeapon].ToggleSelection();

        inventorySelectionCntrl.SetDescription(weaponListItem[currentWeapon].GetWeapon().description);
    }

    public void SetItem(WeaponSO[] weaponList)
    {
        InitializeWeaponsDisplay();

        foreach (WeaponSO weapon in weaponList)
        {
            weaponListItem[nWeapons++].Set(weapon);
        }

        // Select the first button as default
        SelectedButton(0);
    }

    private void InitializeWeaponsDisplay()
    {
        foreach (WeaponsButtonCntrl item in weaponListItem)
        {
            //item.DeActivate();
        }
    }
}