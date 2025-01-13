using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShop : MonoBehaviour
{
    public GameObject shopPanel;
    public void CloseShopPanel()
    {
        shopPanel.SetActive(false);
    }
}
