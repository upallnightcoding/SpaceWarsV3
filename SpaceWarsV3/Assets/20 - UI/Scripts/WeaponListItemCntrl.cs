using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponListItemCntrl : MonoBehaviour
{
    [SerializeField] GameObject highlight;

    public void HightLightItem() => highlight.SetActive(true);

    public void UnHightLightItem() => highlight.SetActive(false);
}
