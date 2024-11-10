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
    [SerializeField] TMP_Text damageText;
    //[SerializeField] TMP_Text descriptionText;

    // Properties ...
    public int WeaponCost { get; set; } = 0;

    private bool selectedSw = false;

    private WeaponSO weapon = null;

    public void HightLightItem() => highlight.SetActive(true);

    private void UnHightLightItem() => highlight.SetActive(false);

    // Return the active weapon
    public WeaponSO GetWeapon() => weapon;

    /**
     * Set() - 
     */
    public void Set(WeaponSO weapon)
    {
        Activate();

        this.weapon = weapon;

        WeaponCost = weapon.cost;
        costText.text = weapon.cost.ToString();
        weaponName.text = weapon.weaponName;
        image.sprite = weapon.sprite;
        damageText.text = weapon.damage.ToString();
    }

    /**
     * ToggleSelection() - Toggle the current selected gameobject.
     */
    public void ToggleSelection()
    {
        selectedSw = !selectedSw;
        selected.SetActive(selectedSw);
    }

    // Return the selected weapon
    public WeaponSO Get() => weapon;

    // Highlight the weapon when the mouse is over the weapon.
    public void OnPointerEnter(PointerEventData eventData) => HightLightItem();

    // Un-Highlight the weapon when the mouse is over the weapon.
    public void OnPointerExit(PointerEventData eventData) => UnHightLightItem();

    private void Activate() => gameObject.SetActive(true);

    public void DeActivate() => gameObject.SetActive(false);
}
