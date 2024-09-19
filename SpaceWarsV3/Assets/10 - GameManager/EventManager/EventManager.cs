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
