using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageCntrl : MonoBehaviour
{
    private float health = 0.0f;
    private float absorb = 0.0f;

    private int enemyId = -1;

    public void Init(float health)
    {
        this.health = health;
    }

    /**
     * Set() - Set the enemy Id, in order to track which enemy has
     * been destroyed.
     */
    public void Set(int enemyId)
    {
        this.enemyId = enemyId;
    }

    /**
     * TurnShieldOn() - If the shield is turned on, then the absorption
     * amount is determined by the shield.  Full absorbtion sets the 
     * absorption parameter to 0.0;
     */
    public void TurnShieldOn(float absorb)
    {
        this.absorb = absorb / 100.0f;
    }

    /**
     * TurnShieldOff() - If the shield is turned off, then there is no
     * absorption and full damage is done to the Fighter.
     */
    public void TurnShieldOff()
    {
        this.absorb = 0.0f;
    }

    /**
     * TakeDamage() - Subtracts the damage value from the health value to
     * determine if the fighter or enemy has been destroyed.  If the 
     * health is less than or equal to 0.0, the object is considered destroyed.
     * The damage value can be augmented if the "absorb" variable is set by 
     * turning on the shield.
     */
    public bool TakeDamage(float damage)
    {
        bool alreadyDead = health <= 0.0f;
        bool nearDeath = false;

        if (!alreadyDead)
        {
            if (damage >= 0.0f)
            {
                health -= damage - (damage * absorb);
            } else
            {
                health += -damage;
                if (health > 100) health = 100;
            }

            Debug.Log($"Health: {health}");

            if (!nearDeath && (health < 30.0f))
            {
                nearDeath = true;
                EventManager.Instance.InvokeOnNearEnemyDeath(enemyId);
            }
        }

        bool justDied = health <= 0.0f; 

        return (!alreadyDead && justDied);
    }

    public void AddHealth(float healing)
    {
        health += healing;
    }

    /**
     * RemainingHealth() - Returns the remaining health of the fighter or
     * enemy.
     */
    public float RemainingHealth()
    {
        return (health);
    }
}
