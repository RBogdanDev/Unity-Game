using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skip : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Dungeon");
    }
}
