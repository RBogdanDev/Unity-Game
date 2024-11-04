using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    public float Amount { get; private set; }
    public Type Type { get; private set; }
    public Response Effect { get; private set; }
    public bool Intterupts { get; private set; }

    public DamageInfo(float amount, Type type, Response effect, bool interrupts)
    {
        Amount = amount;
        Type = type;
        Effect = effect;
        Intterupts = interrupts;
    }

    public string GetDamageType() => Type.ToString();
    public string GetEffect() => Effect.ToString();
}
