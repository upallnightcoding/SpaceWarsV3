using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WeaponListCntrl : MonoBehaviour
{
    [SerializeField] private WeaponListItemCntrl[] weaponListItem;

    [SerializeField] private TMP_Text coinText;

    private int currentToggle = -1;

    private int nWeapons = 0;

    public void SetItem(WeaponSO[] weaponList)
    {
        InitializeWeaponsDisplay();

        foreach (WeaponSO weapon in weaponList)
        {
            weaponListItem[nWeapons++].SetItem(weapon);
        }
    }

    public void SelectedButton(int index)
    {
        if (currentToggle != -1)
        {
            weaponListItem[currentToggle].ToggleSelection();
        }

        currentToggle = index;
        weaponListItem[currentToggle].ToggleSelection();
        AddToCoins(weaponListItem[currentToggle].WeaponCost);
    }

    /**
     * AddToCoins() - 
     */
    private void AddToCoins(int value)
    {
        int total = Int32.Parse(coinText.text);
        total += value;
        coinText.text = total.ToString();
    }

    private void InitializeWeaponsDisplay()
    {
        foreach (WeaponListItemCntrl item in weaponListItem)
        {
            item.DeActivate();
        }
    }
}
