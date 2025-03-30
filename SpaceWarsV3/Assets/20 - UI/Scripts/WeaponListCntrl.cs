using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WeaponListCntrl : MonoBehaviour
{
    [SerializeField] private WeaponListItemCntrl[] weaponListItem;

    [SerializeField] private InventorySelectionCntrl inventorySelectionCntrl;

    private int currentWeapon = -1;

    private int nWeapons = 0;

    // Return the current selected weapon
    public WeaponSO GetCurrentWeapon() => weaponListItem[currentWeapon].GetWeapon();

    /**
     * SetItem() - 
     */
    public void xxSetItem(WeaponSO[] weaponList)
    {
        xxInitializeWeaponsDisplay();

        foreach (WeaponSO weapon in weaponList)
        {
            weaponListItem[nWeapons++].Set(weapon);
        }

        // Select the first button as default
        xxSelectedButton(0);
    }

    /**
     * SelectedButton() - 
     */
    public void xxSelectedButton(int index)
    {
       
        if (currentWeapon != -1)
        {
            weaponListItem[currentWeapon].ToggleSelection();
        }

        currentWeapon = index;
        weaponListItem[currentWeapon].ToggleSelection();

        inventorySelectionCntrl.SetDescription(weaponListItem[currentWeapon].GetWeapon().description);
    }

    /**
     * InitializeWeaponsDisplay() - Initized all of the weapon windows by 
     * deactiviting the window.  The window is only activiated if it is
     * required to be seen.  This means that a weapon must be available
     * to be displayed.
     */
    private void xxInitializeWeaponsDisplay()
    {
        foreach (WeaponListItemCntrl item in weaponListItem)
        {
            item.DeActivate();
        }
    }
}
