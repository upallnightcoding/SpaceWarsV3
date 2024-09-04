using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameData", menuName = "CyberWars/Weapon")]
public class WeaponSO : ScriptableObject
{
    public WeaponType weaponType;

    public string weaponName;

    public Sprite sprite;

    public int cost;
}

public enum WeaponType
{
    AMMO,
    MISSILE,
    SHIELD
}
