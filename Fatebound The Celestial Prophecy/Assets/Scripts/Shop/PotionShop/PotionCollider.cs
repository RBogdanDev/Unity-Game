using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCollider : MonoBehaviour
{
    public Player player;
    public Item item;
    public GameObject potionShop;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            potionShop.SetActive(true);
            player.potionShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
           potionShop.SetActive(false);
           player.potionShop = false;
        }
    }
}
