using UnityEngine;
[CreateAssetMenu(fileName = "Consumable", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public consumableType typeOfConsumable;
    public Player player;
    public int HPRecover;

    public override void Use()
    {
        player = GameObject.FindGameObjectWithTag("DefaultPlayer").GetComponent<Player>();
        switch (typeOfConsumable)
        {
            case consumableType.Potion:
                player.Heal();
                break;
        }
        Inventory.instance.RemoveItem(this, 1);
        Debug.Log($"{name} used and removed from inventory.");
    }

    public enum consumableType { Potion, Food }
   
}
