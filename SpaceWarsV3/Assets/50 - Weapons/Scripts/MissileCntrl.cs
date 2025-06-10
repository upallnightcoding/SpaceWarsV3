using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCntrl : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private GameObject destroyPrefab;

    private GameObject parent;

    private float damage = 10.0f;
    private float radius = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(WeaponSO missile, WeaponSO ammo, GameObject parent)
    {
        this.parent = parent;

        ChooseMissile(missile, ammo);
    }

    private void ChooseMissile(WeaponSO missile, WeaponSO ammo)
    {
        switch(missile.missileType) {
            case MissileType.RANGE:
                StartCoroutine(FireRange(missile, ammo));
                break;
            case MissileType.STAR:
                StartCoroutine(FireStar(missile, ammo));
                break;
            case MissileType.PORTAL:
                StartCoroutine(FirePortal(missile));
                break;
            case MissileType.SEEKING:
                StartCoroutine(FireSeeking(missile, ammo));
                break;
        }
    }

    private IEnumerator FireSeeking(WeaponSO missile, WeaponSO ammo)
    {
        yield return new WaitForSeconds(0.2f);

        Quaternion rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

        if (missile.detonationPrefab)
        {
            GameObject spark = Instantiate(missile.detonationPrefab, transform.position, rotation);
            Destroy(spark, 0.5f);
        }

        Collider[] hitList = Physics.OverlapSphere(transform.position, 100.0f);

        foreach (Collider obstacle in hitList)
        {
            if (obstacle && obstacle.CompareTag("Enemy"))
            {
                if (obstacle.TryGetComponent<TakeDamageCntrl>(out TakeDamageCntrl tdc))
                {
                    Vector3 direction = (obstacle.gameObject.transform.position - transform.position).normalized;
                    FireMissileNow(ammo, direction, 3.0f);

                    yield return null;

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

            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator FirePortal(WeaponSO missile)
    {
        GameObject detonation = null;

        Debug.Log($"missile.detonationPrefab: {missile.detonationPrefab}");

        if (missile.detonationPrefab)
        {
            detonation = Instantiate(missile.detonationPrefab, transform.position, Quaternion.identity);
        }

        for (int i = 1; i <= 4; i++)
        {
            Collider[] hitList = Physics.OverlapSphere(transform.position, radius);

            if (hitList.Length > 0)
            {
                foreach (Collider obstacle in hitList)
                {
                    if ((obstacle != null) && obstacle.CompareTag("Enemy"))
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

                    yield return null;
                }
            }
            
            yield return new WaitForSeconds(1.0f);
        }

        if (missile.detonationPrefab)
        {
            Destroy(detonation);
        }

        Destroy(gameObject);

        yield return null;
    }

    private IEnumerator FireRange(WeaponSO missile, WeaponSO ammo)
    {
        yield return new WaitForSeconds(0.2f);

        Quaternion rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

        if (missile.detonationPrefab)
        {
            GameObject spark = Instantiate(missile.detonationPrefab, transform.position, rotation);
            Destroy(spark, 0.5f);
        }

        Collider[] hitList = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider obstacle in hitList)
        {
            if (obstacle && obstacle.CompareTag("Enemy"))
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

            yield return null;
        }

        Destroy(gameObject);
    }

    /**
     * FireSpark() - 
     */
    private IEnumerator FireStar(WeaponSO missile, WeaponSO ammo)
    {
        yield return new WaitForSeconds(0.2f);

        EventManager.Instance.InvokeOnSound(ammo.ammoSound);

        GameObject spark = Instantiate(missile.detonationPrefab, transform.position, Quaternion.identity);
        Destroy(spark, 0.5f);

        FireMissileNow(ammo, transform.right);
        FireMissileNow(ammo, transform.forward);
        FireMissileNow(ammo, transform.forward + transform.right);
        FireMissileNow(ammo, transform.forward + (-transform.right));

        FireMissileNow(ammo, -transform.right);
        FireMissileNow(ammo, -transform.forward);
        FireMissileNow(ammo, -transform.forward + transform.right);
        FireMissileNow(ammo, -transform.forward + (-transform.right));

        Destroy(gameObject);
    }

    /**
     * FireAmmoNow() - 
     */
    private void FireMissileNow(WeaponSO ammo, Vector3 direction, float offset = 1.0f)
    {
        GameObject weapon = Instantiate(ammo.ammoPrefab, transform.position + direction * offset, Quaternion.identity);
        weapon.GetComponentInChildren<Rigidbody>().AddForce(direction * ammo.force, ForceMode.Impulse);
        weapon.GetComponent<AmmoCntrl>().Initialize(gameData.TAG_FIGHTER, ammo.destroyPrefab, ammo.damage, gameData.sparksPrefab);
        Destroy(weapon, ammo.range);

        EventManager.Instance.InvokeOnSound(ammo.ammoSound);
    }
}

public enum MissileType
{
    STAR,
    RANGE,
    PORTAL,
    SEEKING,
    NONE
}
