using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPortalCntrl : MonoBehaviour
{
    private float damage = 5.0f;

    private void OnTriggerExit(Collider obstacle)
    {
        if (obstacle.CompareTag("Fighter"))
        {
            if (obstacle.TryGetComponent<TakeDamageCntrl>(out TakeDamageCntrl tdc))
            {
                if (tdc.TakeDamage(damage))
                {
                    EventManager.Instance.InvokeOnDestroyFighter();
                }
                else
                {
                    EventManager.Instance.InvokeOnFighterHit(tdc.RemainingHealth());
                }
            }
        }
    }
}
