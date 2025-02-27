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

    public void LoadState(SaveLoadManager data, GameDataSO gameData)
    {
        type = data.type;

        Ammo        = gameData.ammoList[data.ammoIndex];
        Missile     = gameData.missileList[data.missileIndex];
        Shield      = gameData.shieldList[data.shieldIndex];

        Fighter     = gameData.fighterList[data.fighter].fighterPrefab;
    }

    public SaveLoadManager SaveState()
    {
        SaveLoadManager data = new SaveLoadManager();

        data.type           = type;
        data.fighter        = Fighter.GetComponent<FighterCntrl>().getFighterId();
        data.ammoIndex      = Ammo.weaponIndex;
        data.missileIndex   = Missile.weaponIndex;
        data.shieldIndex    = Shield.weaponIndex;

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
