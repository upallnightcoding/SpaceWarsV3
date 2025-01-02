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
    public GameObject destroyPrefab;
    public int maxRounds;
    public float reloadSec;
    public float range;
    public float force;
    public AudioClip ammoSound;

    [Header("Missiles Attributes ...")]
    public GameObject missilePrefab;
    public float missileForce;

    [Header("Shield Attributes ...")]
    public GameObject shieldPrefab;
    public float absorption;
    public float durationSec;
    public float totalDurationSec;
    public int reChargeSec;
}

public enum WeaponType
{
    AMMO,
    MISSILE,
    SHIELD
}
