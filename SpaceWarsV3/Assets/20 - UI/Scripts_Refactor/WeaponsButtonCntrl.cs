using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponsButtonCntrl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject selected;
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private Image image;

    private bool selectedSw = false;

    // Weapon associated with the button
    private WeaponSO weapon = null;

    // Return the active weapon
    public WeaponSO GetWeapon() => weapon;

    private void Activate() => gameObject.SetActive(true);

    public void DeActivate() => gameObject.SetActive(false);

    public void Set(WeaponSO weapon)
    {
        Activate();

        this.weapon = weapon;

        weaponName.text = weapon.weaponName;
        image.sprite = weapon.sprite;
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
