using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponsButtonCntrl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject highlight;
    [SerializeField] GameObject selected;

    private bool selectedSw = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ToggleSelection()
    {
        selectedSw = !selectedSw;
        selected.SetActive(selectedSw);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.SetActive(false);
    }
}
