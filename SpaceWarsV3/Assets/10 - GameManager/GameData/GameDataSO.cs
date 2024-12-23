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

    [Header("Camera Positions ...")]
    public Vector3 cameraEngagementPosition;
    public Vector3 cameraEngagementRotation;

    public Vector3 cameraIdlePosition;
    public Vector3 cameraIdleRotation;

    [Header("Tags ...")]
    public string TAG_ENEMY = "Enemy";
    public string TAG_FIGHTER = "Fighter";
}
