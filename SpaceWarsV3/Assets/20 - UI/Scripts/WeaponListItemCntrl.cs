using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class WeaponListItemCntrl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject highlight;
    [SerializeField] GameObject selected;
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text weaponName;
    [SerializeField] Image image;

    // Properties ...
    public int WeaponCost { get; set; } = 0;

    private bool selectedSw = false;

    public void HightLightItem() => highlight.SetActive(true);

    private void UnHightLightItem() => highlight.SetActive(false);


    public void SetItem(WeaponSO weapon)
    {
        Activate();
        WeaponCost = weapon.cost;
        costText.text = weapon.cost.ToString();
        weaponName.text = weapon.weaponName;
        image.sprite = weapon.sprite;
    }

    public void ToggleSelection()
    {
        selectedSw = !selectedSw;
        selected.SetActive(selectedSw);
    }

    public void OnPointerEnter(PointerEventData eventData) => HightLightItem();

    public void OnPointerExit(PointerEventData eventData) => UnHightLightItem();

    public void Activate() => gameObject.SetActive(true);

    public void DeActivate() => gameObject.SetActive(false);
}
