using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DamageEffect.cs
public struct DamageEffect
{
    public Response Effect { get; private set; }

    private DamageEffect(Response effect)
    {
        Effect = effect;
    }

    public static DamageEffect None => new DamageEffect(Response.None);
    public static DamageEffect Stagger => new DamageEffect(Response.Stagger);
    public static DamageEffect Stun => new DamageEffect(Response.Stun);
    public static DamageEffect KnockBack => new DamageEffect(Response.KnockBack);
    public static DamageEffect Bleed => new DamageEffect(Response.Bleed);
    public static DamageEffect Burn => new DamageEffect(Response.Burn);
    public static DamageEffect Freeze => new DamageEffect(Response.Freeze);

    public override string ToString() => Effect.ToString();
}

