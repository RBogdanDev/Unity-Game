using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInv : MonoBehaviour
{
    public GameObject invpanel;
    public void OpenInventory()
    {
        invpanel.SetActive(true);
    }
}

