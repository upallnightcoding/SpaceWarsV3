using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCntrl : MonoBehaviour
{
    private GameObject destroyPrefab = null;
    private string originator = "";

    public void Initialize(string originator, GameObject destroyPrefab)
    {
        this.destroyPrefab  = destroyPrefab;
        this.originator     = originator;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Ammo Hit Something: ${originator}/${other.tag}/${other.name}");
        if (!other.CompareTag(originator))
        {
            if (other.TryGetComponent<TakeDamageCntrl>(out TakeDamageCntrl tdc))
            {
                if (tdc.TakeDamage(10.0f))
                {
                    if (destroyPrefab)
                    {
                        GameObject prefab = Instantiate(destroyPrefab, transform.position, Quaternion.identity);
                        Destroy(prefab, 4.0f);
                    }

                    Destroy(other.transform.gameObject);

                    EventManager.Instance.InvokeOnDestroyEnemy();
                } else
                {
                    EventManager.Instance.InvokeOnFighterHit();
                }

                Destroy(gameObject);
            }
        }
    }
}
