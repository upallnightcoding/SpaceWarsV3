using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WeaponListCntrl : MonoBehaviour
{
    [SerializeField] private WeaponListItemCntrl[] weaponListItem;

    [SerializeField] private TMP_Text coinText;

    private int currentWeapon = -1;

    private int nWeapons = 0;

    /**
     * SetItem() - 
     */
    public void SetItem(WeaponSO[] weaponList)
    {
        InitializeWeaponsDisplay();

        foreach (WeaponSO weapon in weaponList)
        {
            weaponListItem[nWeapons++].Set(weapon);
        }
    }

    /**
     * SelectedButton() - 
     */
    public void SelectedButton(int index)
    {
        if (currentWeapon != -1)
        {
            AddToCoins(weaponListItem[currentWeapon].WeaponCost);
            weaponListItem[currentWeapon].ToggleSelection();
        }

        currentWeapon = index;
        weaponListItem[currentWeapon].ToggleSelection();
        AddToCoins(-weaponListItem[currentWeapon].WeaponCost);
    }

    /**
     * AddToCoins() - Adds the value to the current coin value in the 
     * UI display.  The current coin value must be converted from 
     * text before the value can be added.
     */
    private void AddToCoins(int value)
    {
        int total = Int32.Parse(coinText.text) + value;
        coinText.text = total.ToString();
    }

    /**
     * InitializeWeaponsDisplay() - 
     */
    private void InitializeWeaponsDisplay()
    {
        foreach (WeaponListItemCntrl item in weaponListItem)
        {
            item.DeActivate();
        }
    }

    public WeaponSO Get() => weaponListItem[currentWeapon].Get();
}
