using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "CyberWars/Game Data")]
public class GameDataSO : ScriptableObject
{
    [Header("Display Attributes")]
    public Vector3 mainMenuFighterCenter;
    public Vector3 fighterSelectionCenter;

    [Header("Fighters ...")]
    public FighterSO[] fighterList;
    public GameObject sparksPrefab;
    public Vector3 fighterOffset;

    [Header("Camera Positions ...")]
    public Vector3 cameraEngagementPosition;
    public Vector3 cameraEngagementRotation;

    [Header("Enemy Attack ...")]
    public int lowAttack;
    public int medAttack;
    public int hghAttack;
    public int[] enemyAttackLevel;

    [Header("Camera Id ...")]
    public Vector3 cameraIdlePosition;
    public Vector3 cameraIdleRotation;

    [Header("Tags ...")]
    public string TAG_ENEMY = "Enemy";
    public string TAG_FIGHTER = "Fighter";

    [Header("Weapon List ...")]
    [SerializeField] public WeaponSO[] ammoList;
    [SerializeField] public WeaponSO[] missileList;
    [SerializeField] public WeaponSO[] shieldList;

    [Header("Enemy AI ...")]
    public float combatRadius = 75.0f;

}
