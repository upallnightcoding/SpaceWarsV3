using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectLevelMenuOptionCntrl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject button;

    public void OnPointerEnter(PointerEventData eventData) 
    {
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
