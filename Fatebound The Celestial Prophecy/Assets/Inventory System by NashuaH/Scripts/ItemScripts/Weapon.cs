
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon", menuName = "Item/Weapon")]
public class Weapon : Item
{
    public weaponType type;
    public int weaponDamage;

    public override void Use()
    {

        
    }

    public enum weaponType { Sword, Axe, Shield}
}
