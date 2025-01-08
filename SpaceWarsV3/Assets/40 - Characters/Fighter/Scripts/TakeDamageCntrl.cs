using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageCntrl : MonoBehaviour
{
    private float health = 0.0f;
    private float absorb = 0.0f;

    private int index = -1;

    public void Init(float initHealth)
    {
        health = initHealth;
    }

    public void Set(int index)
    {
        this.index = index;
    }

    /**
     * TurnShieldOn() - If the shield is turned on, then the absorption
     * amount is determined by the shield.  Full absorbtion sets the 
     * absorption parameter to 0.0;
     */
    public void TurnShieldOn(float absorb)
    {
        this.absorb = absorb;
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

        if (!alreadyDead)
        {
            health -= damage - (damage * absorb);
        }

        bool justDied = health <= 0.0f; 

        Debug.Log($"index: ({index}) alreadyDead: {alreadyDead} health: {health} damage: {damage} justDied: {justDied}");

        return (!alreadyDead && justDied);
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
