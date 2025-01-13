using UnityEngine;
[CreateAssetMenu(fileName = "Resource", menuName = "Item/Resource")]
public class Resource : Item
{
    public resourceType type;
    public Player player;

    public override void Use()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        player.Heal();
        Inventory.instance.RemoveItem(this, 1);
        Debug.Log($"{name} used and removed from inventory.");
    }

    public enum resourceType { Food, HP, MP }
}

