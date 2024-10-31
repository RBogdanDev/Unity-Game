using System.Collections;
using System.Collections.Generic;
// Damage.cs
using UnityEngine;

public class Damage : MonoBehaviour
{
    public DamageType damageType = DamageType.None;
    public DamageEffect damageEffect = DamageEffect.None;
    public float damageAmount = 20;

    private void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
            ApplyEffect(damageable);
        }
    }

    private void ApplyEffect(IDamageable target)
    {
        //Logica pentru aplicarea efectului specific, ex: `Stagger`, `Burn` etc.
    }
}
