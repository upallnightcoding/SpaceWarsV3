using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponListCntrl : MonoBehaviour
{
    [SerializeField] private WeaponListItemCntrl weaponListIten;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SelectedButton0()
    {
        Debug.Log("Button 0");
    }
}
