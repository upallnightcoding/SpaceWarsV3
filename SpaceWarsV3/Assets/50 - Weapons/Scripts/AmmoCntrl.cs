using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCntrl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TakeDamageCntrl>(out TakeDamageCntrl tdc)) 
        {
            if (tdc.TakeDamage(10.0f))
            {
                Destroy(other.transform.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
