using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData 
{
    public GameObject Fighter { get; set; }

    public WeaponSO Ammo { get; set; }
    public WeaponSO Missile { get; set; }
    public WeaponSO Shield { get; set; }
    public int Level { get; set; } = 1;

    private LevelType type = LevelType.TUTORIAL;

    public LevelData(LevelType type)
    {
        this.type = type;
    }

    /**
     * GetLevel() - Returns the current level of the player.
     */
    public LevelType GetLevelType()
    {
        return (type);
    }

    /**
     * IsBerserk() - Returns a true if the game play is in the "Berserk"
     * mode.  A false is returned  otherwise.
     */
    public bool IsBerserk()
    {
        return (this.type == LevelType.BERSERK);
    }

    /**
     * LoadState() - 
     */
    public void LoadState(SaveLoadObject data, UICntrl uiCntrl)
    {
        type = data.type;

        Ammo        = uiCntrl.gameData.ammoList[data.ammoIndex];
        Missile     = uiCntrl.gameData.missileList[data.missileIndex];
        Shield      = uiCntrl.gameData.shieldList[data.shieldIndex];

        uiCntrl.ammoListCntrl.SelectedButton(data.ammoIndex);
        uiCntrl.missileListCntrl.SelectedButton(data.missileIndex);
        uiCntrl.shieldListCntrl.SelectedButton(data.shieldIndex);

        Fighter = uiCntrl.gameData.fighterList[data.fighter].fighterPrefab;

        Level = data.level;

        uiCntrl.SetGamePlayLevel(Level);
    }

    /**
     * SaveState() - 
     */
    public SaveLoadObject SaveState()
    {
        SaveLoadObject data = new SaveLoadObject();

        data.type           = type;

        data.ammoIndex      = Ammo.weaponIndex;
        data.missileIndex   = Missile.weaponIndex;
        data.shieldIndex    = Shield.weaponIndex;

        data.fighter        = Fighter.GetComponent<FighterCntrl>().getFighterId();

        data.level = Level;

        return (data);
    }
}

public enum LevelType
{
    TUTORIAL = 0,
    HAVOC,
    TOOEASY,
    BERSERK,
    LEVEL,
    UNKNOWN
}
