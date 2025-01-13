using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInv : MonoBehaviour
{
    public GameObject invpanel;
    public void CloseInventory()
    {
        invpanel.SetActive(false);
    }
}
