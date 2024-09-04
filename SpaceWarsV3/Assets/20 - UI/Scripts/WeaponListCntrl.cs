using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponListCntrl : MonoBehaviour
{
    [SerializeField] private WeaponListItemCntrl[] weaponListIten;

    [SerializeField] private WeaponSO[] ammo;

    private int currentToggle = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (WeaponListItemCntrl item in weaponListIten)
        {
            item.DeActivate();
        }

        weaponListIten[currentToggle].ToggleSelection();

        weaponListIten[0].SetItem(ammo[0]);
        weaponListIten[1].SetItem(ammo[1]);
        weaponListIten[2].SetItem(ammo[2]);
    }

    public void SelectedButton(int index)
    {
        weaponListIten[currentToggle].ToggleSelection();
        weaponListIten[index].ToggleSelection();
        currentToggle = index;
    }
}
