using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItems : MonoBehaviour
{
    public Player player;
    public Item[] itemsForSale;
    private int[] itemCosts = { 50, 100, 80, 120, 200, 150 };
    private int[] itemQuantities;

    private void Start()
    {
        itemQuantities = new int[itemsForSale.Length];
    }

    public void BuyItem(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= itemCosts.Length || itemIndex >= itemsForSale.Length)
        {
            Debug.LogWarning("Invalid item index.");
            return;
        }

        int itemCost = itemCosts[itemIndex];
        Item itemToBuy = itemsForSale[itemIndex];

        if (player.coins >= itemCost)
        {
            player.coins -= itemCost;
            itemQuantities[itemIndex]++;

            Inventory.instance.AddItem(itemToBuy, 1);

            Debug.Log($"Purchased {itemToBuy.itemName}. Remaining Coins: {player.coins}");
        }
        else
        {
            Debug.Log("Not enough coins to purchase this item.");
        }
    }
}
