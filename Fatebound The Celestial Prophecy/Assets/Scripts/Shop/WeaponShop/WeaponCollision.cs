using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Player player = other.GetComponent<Player>(); 
            if (player != null)
            {
                player.SetShopTag("Weapons");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.SetShopTag("None");
            }
        }
    }
}
