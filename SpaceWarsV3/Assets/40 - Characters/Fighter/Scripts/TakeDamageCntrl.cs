using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageCntrl : MonoBehaviour
{
    private float health = 0.0f;
    private float absorb = 0.0f;

    public void Init(float initHealth)
    {
        health = initHealth;
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
        health -= damage - (damage * absorb);

        return (health <= 0.0f);
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
