using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonToBoss : MonoBehaviour
{
    public SaveInventorySystem saveFileSystem;
   public Player player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            saveFileSystem.SaveInventory();
            SceneManager.LoadScene("Final_Boss");
        }
    }
}

