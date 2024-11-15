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
}
