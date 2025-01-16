using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCntrl : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private GameObject sparkPrefab;
    [SerializeField] private GameObject destroyPrefab;

    private float damage = 40.0f;
    private float radius = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(WeaponSO ammo)
    {
        StartCoroutine(FireRange(ammo));
    }

    private IEnumerator FireRange(WeaponSO ammo)
    {
        yield return new WaitForSeconds(0.2f);

        Quaternion rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

        GameObject spark = Instantiate(sparkPrefab, transform.position, rotation);
        Destroy(spark, 0.5f);

        Collider[] hitList = Physics.OverlapSphere(transform.position, radius);

        Debug.Log($"Hit List: {hitList.Length}");

        foreach(Collider obstacle in hitList)
        {
            Debug.Log($"Obstacle: {obstacle.tag}");

            if (obstacle.CompareTag("Enemy"))
            {
                if (obstacle.TryGetComponent<TakeDamageCntrl>(out TakeDamageCntrl tdc))
                {
                    if (tdc.TakeDamage(damage))
                    {
                        if (destroyPrefab)
                        {
                            GameObject prefab = Instantiate(destroyPrefab, transform.position, Quaternion.identity);
                            Destroy(prefab, 4.0f);
                        }

                        EventManager.Instance.InvokeOnDestroyEnemyShip();

                        Destroy(obstacle.transform.gameObject);
                    }
                }
            }
        }

        Destroy(gameObject);
    }

    /**
     * FireSpark() - 
     */
    private IEnumerator FireSpark(WeaponSO ammo)
    {
        yield return new WaitForSeconds(0.2f);

        GameObject spark = Instantiate(sparkPrefab, transform.position, Quaternion.identity);
        Destroy(spark, 0.5f);

        FireAmmoNow(ammo, transform.right);
        FireAmmoNow(ammo, transform.forward);
        FireAmmoNow(ammo, transform.forward + transform.right);
        FireAmmoNow(ammo, transform.forward + (-transform.right));

        FireAmmoNow(ammo, -transform.right);
        FireAmmoNow(ammo, -transform.forward);
        FireAmmoNow(ammo, -transform.forward + transform.right);
        FireAmmoNow(ammo, -transform.forward + (-transform.right));

        Destroy(gameObject);
    }

    /**
     * FireAmmoNow() - 
     */
    private void FireAmmoNow(WeaponSO ammo, Vector3 direction)
    {
        GameObject missile = Instantiate(ammo.ammoPrefab, transform.position + direction * 0.5f, Quaternion.identity);
        missile.GetComponentInChildren<Rigidbody>().AddForce(direction * ammo.force, ForceMode.Impulse);
        missile.GetComponent<AmmoCntrl>().Initialize(gameData.TAG_FIGHTER, ammo.destroyPrefab, ammo.damage, ammo.ammoSound);
        Destroy(missile, ammo.range);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(gameObject.transform.position, radius);
        Debug.Log("Drawing Gizmos ...");
    }
}
