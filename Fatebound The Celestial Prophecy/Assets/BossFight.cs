using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BossFight : Enemy
{
    private float SecondPhase = 100; // Pragul la care bossul devine mai agresiv
    private bool isRaging;
    private Animator animator;
    new void Start()
    {
        base.Start();
        // Inițializare specifică bossului
        isRaging = false;
    }

    new void Update()
    {
        base.Update();

        // Comportament special pentru boss, de exemplu, când ajunge sub un anumit procent din viață
        if (Health <= SecondPhase && !isRaging)
        {
            isRaging = true;
            // Activăm comportamente speciale
            ActivateRage();
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            XPManager.Instance.AddXP(xpAmount);
        }
    }

    private void ActivateRage()
    {
        // Crește puterea atacului.
        damage *= 2;
        duration /= 2;
    }

}