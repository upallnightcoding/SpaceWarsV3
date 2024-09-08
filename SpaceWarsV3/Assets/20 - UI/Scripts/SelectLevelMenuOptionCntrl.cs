using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectLevelMenuOptionCntrl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image icon;

    public void OnPointerEnter(PointerEventData eventData) 
    {
        icon.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        icon.enabled = false;
    }
}
