using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCntrl : MonoBehaviour
{
    private GameObject destroyPrefab = null;
    private string originator = "";
    private bool active;
    private float damage;

    public void Initialize(string originator, GameObject destroyPrefab, float damage)
    {
        this.destroyPrefab  = destroyPrefab;
        this.originator     = originator;
        this.damage         = damage;

        active = true;
    }

    /**
     * OnTriggerExit() - 
     */
    private void OnTriggerExit(Collider obstacle)
    {
        if (!obstacle.CompareTag(originator) && active)
        {
            active = false;

            if (obstacle.TryGetComponent<TakeDamageCntrl>(out TakeDamageCntrl tdc))
            {
                if (tdc.TakeDamage(damage))
                {
                    if (destroyPrefab)
                    {
                        GameObject prefab = Instantiate(destroyPrefab, transform.position, Quaternion.identity);
                        Destroy(prefab, 4.0f);
                    }

                    switch (obstacle.tag)
                    {
                        case "Enemy":
                            EventManager.Instance.InvokeOnDestroyEnemyShip();
                            break;
                        case "Fighter":
                            EventManager.Instance.InvokeOnDestroyFighter();
                            break;
                    }

                    Destroy(obstacle.transform.gameObject);
                } else
                {
                    switch(obstacle.tag)
                    {
                        case "Fighter":
                            EventManager.Instance.InvokeOnFighterHit(tdc.RemainingHealth());
                            break;
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
