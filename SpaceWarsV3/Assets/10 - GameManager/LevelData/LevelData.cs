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

    public bool IsBerserk()
    {
        return (this.type == LevelType.BERSERK);
    }

    public void LoadState(SaveLoadManager data)
    {
        type = data.type;
    }

    public SaveLoadManager SaveState()
    {
        SaveLoadManager data = new SaveLoadManager();

        data.type       = type;
        data.fighter    = Fighter.name;
        data.ammo       = Ammo.name;
        data.missile    = Missile.name;
        data.shield     = Shield.name;

        return (data);
    }
}

public enum LevelType
{
    TUTORIAL = 0,
    HAVOC,
    TOOEASY,
    BERSERK,
    LEVEL
}
