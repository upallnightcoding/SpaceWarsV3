using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCntrl : MonoBehaviour
{
    [SerializeField] private WeaponSO ammo;
    [SerializeField] private GameObject firePoint;

    private int ammoCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        ammoCount = ammo.maxRounds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FireMissle()
    {
        GameObject go = Instantiate(ammo.ammoPrefab, firePoint.transform.position, transform.rotation);
        go.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * ammo.force, ForceMode.Impulse);
        Destroy(go, ammo.range);

        ammoCount -= 1;

        //EventManager.Instance.InvokeOnUpdateAmmoBar(ammoCount, maxAmmoCount);

        //if (ammoCount == 0)
        //{
          //  StartCoroutine(ReLoad());
        //}

        //yield return new WaitForSeconds(0.1f);
    }

}
