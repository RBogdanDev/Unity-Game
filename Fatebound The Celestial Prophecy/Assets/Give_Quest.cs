using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Give_Quest : MonoBehaviour
{
    public Quest get()
    {
        GameObject gamePlayer = GameObject.FindGameObjectWithTag("Player");

        if (gamePlayer != null)
        {
            Player player = gamePlayer.GetComponent<Player>();
            if (player.currentQuest == null && Vector2.Distance(player.transform.position, transform.position) < 2)
            {
                Quest quest = this.gameObject.GetComponent<Quest>();
                if (quest != null)
                {
                    return quest;
                }
            }
        }
        
        return null;
    }
}
