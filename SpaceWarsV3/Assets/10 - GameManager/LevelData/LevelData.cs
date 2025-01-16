using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData 
{
    public GameObject Fighter { get; set; }

    public WeaponSO Ammo { get; set; }
    public WeaponSO Missile { get; set; }
    public WeaponSO Shield { get; set; }

    private LevelType type = LevelType.TUTORIAL;

    private int level = 1;

    public LevelData(LevelType type)
    {
        this.type = type;
    }

    /**
     * GetLevel() - Returns the current level of the player.
     */
    public LevelType GetLevel()
    {
        return (type);
    }

    public bool isBerserk()
    {
        return (this.type == LevelType.BERSERK);
    }
}

public enum LevelType
{
    TUTORIAL,
    HAVOC,
    TOOEASY,
    BERSERK,
    LEVEL
}
