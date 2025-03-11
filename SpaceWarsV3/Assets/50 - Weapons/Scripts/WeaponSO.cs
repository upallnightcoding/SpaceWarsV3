using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameData", menuName = "CyberWars/Weapon")]
public class WeaponSO : ScriptableObject
{
    public WeaponType weaponType;

    public string weaponName;

    public int weaponIndex = -1;

    [TextArea(5, 5)]
    public string description;

    public Sprite sprite;

    public int damage;

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
    public MissileType missileType = MissileType.NONE;
    public GameObject detonationPrefab;

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
