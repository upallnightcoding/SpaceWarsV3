using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCntrl : MonoBehaviour
{
    private GameObject destroyPrefab = null;
    private string originator = "";
    private bool active;
    private float damage;
    private GameObject sparksPrefab = null;

    public void Initialize(
        string originator, 
        GameObject destroyPrefab, 
        float damage, 
        GameObject sparksPrefab = null
    )
    {
        this.destroyPrefab  = destroyPrefab;
        this.originator     = originator;
        this.damage         = damage;
        this.sparksPrefab   = sparksPrefab;

        active = true;

        Debug.Log($"Initialize: {originator}");
    }

    /**
     * OnTriggerExit() - 
     */
    private void OnTriggerEnter(Collider obstacle)
    {
        Debug.Log($"OnTriggerEnter: {originator}/{obstacle.tag}");
        if (!obstacle.CompareTag(originator) && active && !obstacle.CompareTag("Untagged"))
        {
            active = false;

            if (obstacle.TryGetComponent<TakeDamageCntrl>(out TakeDamageCntrl tdc))
            {
                Debug.Log($"obstacle.TryGetComponent: {obstacle.tag}");
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
                            Destroy(obstacle.transform.gameObject);
                            break;
                        case "Fighter":
                            EventManager.Instance.InvokeOnFighterHit(tdc.RemainingHealth());
                            EventManager.Instance.InvokeOnDestroyFighter();
                            break;
                    }
                } else
                {
                    if (sparksPrefab)
                    {
                        GameObject prefab = Instantiate(sparksPrefab, transform.position, Quaternion.identity);
                        prefab.transform.localScale = new Vector3(15.0f, 15.0f, 15.0f);
                        Destroy(prefab, 0.5f);
                    }

                    switch (obstacle.tag)
                    {
                        case "Fighter":
                            EventManager.Instance.InvokeOnFighterHit(tdc.RemainingHealth());
                            break;
                    }
                }
            }

            Destroy(gameObject);
            //Debug.Log($"Destroy Game Object");
        }
    }
}
