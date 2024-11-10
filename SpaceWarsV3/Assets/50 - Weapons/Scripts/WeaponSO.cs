using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameData", menuName = "CyberWars/Weapon")]
public class WeaponSO : ScriptableObject
{
    public WeaponType weaponType;

    public string weaponName;

    [TextArea(5, 5)]
    public string description;

    public Sprite sprite;

    public int cost;

    public int damage;

    public float rangeSec;

    [Header("Ammo Attributes ...")]
    public GameObject ammoPrefab;
    public int maxRounds;
    public float reloadSec;
    public float range;
    public float force;

    [Header("Missiles Attributes ...")]
    public int payload;
    public float damageRange;
    public GameObject missileExplosion;

    [Header("Shield Attributes ...")]
    public int defectionPercent;
}

public enum WeaponType
{
    AMMO,
    MISSILE,
    SHIELD
}
