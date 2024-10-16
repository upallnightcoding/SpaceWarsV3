using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCntrl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ammo has been triggered ...");
    }
}
