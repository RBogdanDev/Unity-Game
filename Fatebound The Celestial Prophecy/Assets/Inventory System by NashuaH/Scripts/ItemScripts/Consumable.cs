using UnityEngine;
[CreateAssetMenu(fileName = "Consumable", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public consumableType typeOfConsumable;
    public Player player;
    public int HPRecover;

    public override void Use()
    {
        
    }

    public enum consumableType { Potion, Food }
   
}
