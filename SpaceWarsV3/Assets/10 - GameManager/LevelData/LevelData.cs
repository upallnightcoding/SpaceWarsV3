using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData 
{
    public GameObject Fighter { get; set; }
    public WeaponSO Ammo { get; set; }

    private LevelType type = LevelType.TUTORIAL;

    private int level = -1;

    public LevelData(LevelType type)
    {
        this.type = type;
    }
}

public enum LevelType
{
    TUTORIAL,
    HAVOC,
    TOOEASY,
    LEVEL
}
