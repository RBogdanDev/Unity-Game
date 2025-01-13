using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl2ToDungeon : MonoBehaviour
{
    public Player player;
    public GameObject inv;
    private void OnTriggerEnter2D(Collider2D other)
    {
        SaveInventorySystem saveSystem = inv.GetComponent<SaveInventorySystem>();

        if (other.CompareTag("Player")) 
        {
            saveSystem.SaveInventory();
            SceneManager.LoadScene("Dungeon");
        }
    }
}
