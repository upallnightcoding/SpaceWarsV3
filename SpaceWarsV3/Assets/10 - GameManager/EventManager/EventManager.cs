using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager
{
    public event Action OnNewGameAction = delegate { };
    public void InvokeOnNewGameAction() => OnNewGameAction.Invoke();

    public event Action OnSetLevelTutorial = delegate { };
    public void InvokeOnSetLevelTutorial() => OnSetLevelTutorial.Invoke();

    public event Action<GameObject> OnFighterSelection = delegate { };
    public void InvokeOnFighterSelection(GameObject fighter) => OnFighterSelection.Invoke(fighter);

    public event Action OnStartEngagement = delegate { };
    public void InvokeOnStartEngagement() => OnStartEngagement.Invoke();

    public event Action<Vector2> OnInputMove = delegate { };
    public void InvokeOnInputMove(Vector2 context) => OnInputMove.Invoke(context);

    public event Action<Vector2> OnInputLook = delegate { };
    public void InvokeOnInputLook(Vector2 context) => OnInputLook.Invoke(context);

    public event Action OnFire = delegate { };
    public void InvokeOnFire() => OnFire.Invoke();

    public event Action<int> OnFireKey = delegate { };
    public void InvokeOnFireKey(int key) => OnFireKey.Invoke(key);

    public event Action<int, int> OnUpdateAmmoBar = delegate { };
    public void InvokeOnUpdateAmmoBar(int ammoCount, int maxAmmoCount) => OnUpdateAmmoBar.Invoke(ammoCount, maxAmmoCount);

    public event Action<float, float> OnUpdateReload = delegate { };
    public void InvokeOnUpdateReload(float timing, float reloadTime) => OnUpdateReload.Invoke(timing, reloadTime);

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
