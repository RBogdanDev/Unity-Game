using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl2ToDungeon : MonoBehaviour
{
    public Player player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            FindObjectOfType<SaveInventorySystem>().SaveInventory();
            SceneManager.LoadScene("Dungeon");
        }
    }
}
