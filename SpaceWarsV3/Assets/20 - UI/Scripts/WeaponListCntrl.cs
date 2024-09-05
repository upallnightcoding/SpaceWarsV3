using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponListCntrl : MonoBehaviour
{
    [SerializeField] private WeaponListItemCntrl[] weaponListItem;

    private int currentToggle = 0;

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
        weaponListItem[currentToggle].ToggleSelection();
        weaponListItem[index].ToggleSelection();
        currentToggle = index;
    }

    private void InitializeWeaponsDisplay()
    {
        foreach (WeaponListItemCntrl item in weaponListItem)
        {
            item.DeActivate();
        }

        weaponListItem[currentToggle].ToggleSelection();
    }
}
