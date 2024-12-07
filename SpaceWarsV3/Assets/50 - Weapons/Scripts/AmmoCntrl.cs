using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCntrl : MonoBehaviour
{
    private GameObject destroyPrefab;

    public void Initialize(GameObject destroyPrefab)
    {
        this.destroyPrefab = destroyPrefab;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter ...");
        if (other.TryGetComponent<TakeDamageCntrl>(out TakeDamageCntrl tdc)) 
        {
            Debug.Log("TakeDamageCntrl");
            if (tdc.TakeDamage(10.0f))
            {
                Debug.Log("Create Explosion");
                GameObject prefab = Instantiate(destroyPrefab, transform.position, Quaternion.identity);
                Destroy(prefab, 4.0f);
                Destroy(other.transform.gameObject);
                EventManager.Instance.InvokeOnDestroyEnemy();
            }

            Destroy(gameObject);
        }
    }
}
