using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventorySelectionCntrl : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetDescription(string description)
    {
        descriptionWeapon.text = description;
    }
}
