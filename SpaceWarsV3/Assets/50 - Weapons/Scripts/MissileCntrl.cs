using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCntrl : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private GameObject separatorPrefab;

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
        StartCoroutine(FireAmmo(ammo));
    }

    private IEnumerator FireAmmo(WeaponSO ammo)
    {
        yield return new WaitForSeconds(0.2f);

        GameObject separator = Instantiate(separatorPrefab, transform.position, Quaternion.identity);

        FireAmmoNow(ammo, transform.forward);
        FireAmmoNow(ammo, transform.forward + transform.right);
        FireAmmoNow(ammo, transform.forward + (-transform.right));
        FireAmmoNow(ammo, transform.right);
        FireAmmoNow(ammo, (-transform.right));

        Destroy(gameObject);
        Destroy(separator);
    }

    private void FireAmmoNow(WeaponSO ammo, Vector3 direction)
    {
        GameObject missile = Instantiate(ammo.ammoPrefab, transform.position + direction * 0.5f, transform.rotation);
        missile.GetComponentInChildren<Rigidbody>().AddForce(direction * ammo.force, ForceMode.Impulse);
        missile.GetComponent<AmmoCntrl>().Initialize(gameData.TAG_FIGHTER, ammo.destroyPrefab, ammo.damage, ammo.ammoSound);
        Destroy(missile, ammo.range);
    }
}
