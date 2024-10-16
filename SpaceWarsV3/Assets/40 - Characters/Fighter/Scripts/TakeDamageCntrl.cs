using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageCntrl : MonoBehaviour
{
    private float totalDamage = 20.0f;

    public bool TakeDamage(float damage)
    {
        totalDamage -= damage;

        return (totalDamage <= 0.0f);
    }
}
