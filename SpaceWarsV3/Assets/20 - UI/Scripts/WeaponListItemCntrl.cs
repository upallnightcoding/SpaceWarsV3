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
    [SerializeField] TMP_Text weaponName;
    [SerializeField] Image image;

    // Properties ...
    private bool selectedSw = false;

    // Weapon associated with the button
    private WeaponSO weapon = null;

    // Turn-On the item image highlight
    public void HighLightItem() => highlight.SetActive(true);

    // Turn-Off the item image highlight
    private void UnHighLightItem() => highlight.SetActive(false);

    // Return the active weapon
    public WeaponSO GetWeapon() => weapon;

    /**
     * Set() - 
     */
    public void Set(WeaponSO weapon)
    {
        Activate();

        this.weapon = weapon;

        weaponName.text = weapon.weaponName;
        image.sprite = weapon.sprite;
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
    public void OnPointerEnter(PointerEventData eventData) => HighLightItem();

    // Un-Highlight the weapon when the mouse is over the weapon.
    public void OnPointerExit(PointerEventData eventData) => UnHighLightItem();

    private void Activate() => gameObject.SetActive(true);

    public void DeActivate() => gameObject.SetActive(false);
}
