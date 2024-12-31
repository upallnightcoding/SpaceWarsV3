using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager
{
    // This event is raise if the player wins.
    //----------------------------------------
    public event Action OnPlayerWins = delegate { };
    public void InvokeOnPlayerWins() => OnPlayerWins.Invoke();

    // If the fighter takes a hit, update the UI and other stuff.
    //-----------------------------------------------------------
    public event Action<float> OnFighterHit = delegate { };
    public void InvokeOnFighterHit(float remainingDamage) => OnFighterHit.Invoke(remainingDamage);

    // When an enemy ship has been destroyed send out this event.
    //-----------------------------------------------------------
    public event Action OnDestroyEnemyShip = delegate { };
    public void InvokeOnDestroyEnemyShip() => OnDestroyEnemyShip.Invoke();

    // When a game is over, all ships should be destroyed.
    //----------------------------------------------------
    public event Action OnDestroyAllShips = delegate { };
    public void InvokeDestroyAllShips() => OnDestroyAllShips.Invoke();

    // When the fighter has been destroyed send out this event.
    //---------------------------------------------------------
    public event Action OnPlayerLooses = delegate { };
    public void InvokeOnDestroyFighter() => OnPlayerLooses.Invoke();

    public event Action OnNewGameAction = delegate { };
    public void InvokeOnNewGameAction() => OnNewGameAction.Invoke();

    public event Action<GameObject> OnFighterSelection = delegate { };
    public void InvokeOnFighterSelection(GameObject fighter) => OnFighterSelection.Invoke(fighter);

    // Ammo, shield, Missle Bars Update
    //---------------------------------
    public event Action<float, float> OnUpdateShield = delegate { };
    public void InvokeOnUpdateShield(float shield, float maxShiled) => OnUpdateShield.Invoke(shield, maxShiled);

    public event Action<int, int> OnUpdateAmmoBar = delegate { };
    public void InvokeOnUpdateAmmoBar(int ammoCount, int maxAmmoCount) => OnUpdateAmmoBar.Invoke(ammoCount, maxAmmoCount);

    public event Action<float, float> OnUpdateReload = delegate { };
    public void InvokeOnUpdateReload(float timing, float reloadTime) => OnUpdateReload.Invoke(timing, reloadTime);

    public event Action<LevelData> OnStartBattle = delegate { };
    public void InvokeOnStartBattle(LevelData levelData) => OnStartBattle.Invoke(levelData);

    /*******************************/
    /*** Keyboard Related Events ***/
    /*******************************/

    public event Action<Vector2> OnInputMove = delegate { };
    public void InvokeOnInputMove(Vector2 position) => OnInputMove.Invoke(position);

    public event Action<Vector2> OnInputLook = delegate { };
    public void InvokeOnInputLook(Vector2 position) => OnInputLook.Invoke(position);

    public event Action OnFire = delegate { };
    public void InvokeOnFire() => OnFire.Invoke();

    public event Action<int> OnFireKey = delegate { };
    public void InvokeOnFireKey(int key) => OnFireKey.Invoke(key);


    // Event Manager Singleton
    //========================
    public static EventManager Instance
    {
        get
        {
            if (aInstance == null)
            {
                aInstance = new EventManager();
            }

            return (aInstance);
        }
    }

    public static EventManager aInstance = null;
}
