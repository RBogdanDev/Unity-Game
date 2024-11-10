using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    public float Amount { get; private set; }
    public Type Type { get; private set; }
    public Response Effect { get; private set; }
    public float EffectDuration { get; private set; }
    public float EffectDamage { get; private set; }
    public bool Intterupts { get; private set; }

    public DamageInfo(float amount, Type type, Response effect, float duration, float damage, bool interrupts)
    {
        Amount = amount;
        Type = type;
        Effect = effect;
        EffectDuration = duration;
        EffectDamage = damage;
        Intterupts = interrupts;
    }

    public void Update(float amount, float duration, float damage)
    {
        Amount = amount;
        EffectDuration = duration;
        EffectDamage = damage;
    }
}
