using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageType
{
    public Type Type { get; private set; }

    private DamageType(Type type)
    {
        Type = type;
    }

    public static DamageType None => new DamageType(Type.None);
    public static DamageType Melee => new DamageType(Type.Melee);
    public static DamageType Projectile => new DamageType(Type.Projectile);
    public static DamageType Magic => new DamageType(Type.Magic);

    public override string ToString() => Type.ToString();
}

