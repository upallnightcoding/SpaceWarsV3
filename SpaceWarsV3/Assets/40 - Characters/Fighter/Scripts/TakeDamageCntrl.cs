using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageCntrl : MonoBehaviour
{
    private float health = 0.0f;

    public void Init(float initHealth)
    {
        health = initHealth;
    }

    public bool TakeDamage(float damage)
    {
        health -= damage;

        return (health <= 0.0f);
    }
}
