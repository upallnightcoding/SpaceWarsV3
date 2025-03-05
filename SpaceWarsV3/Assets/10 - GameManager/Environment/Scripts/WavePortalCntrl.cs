using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePortalCntrl : MonoBehaviour
{
    private float damage = -10.0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider obstacle)
    {
        Debug.Log($"On Trigger Exit WavePortalCntrl: {obstacle.gameObject.tag}");

        if (obstacle.CompareTag("Fighter"))
        {
            if (obstacle.TryGetComponent<TakeDamageCntrl>(out TakeDamageCntrl tdc))
            {
                tdc.TakeDamage(damage);
                EventManager.Instance.InvokeOnFighterHit(tdc.RemainingHealth());
            }
        }
    }
}
