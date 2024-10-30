using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public Player pDamage;
    public Enemy eDamage;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Aici ar trebui sa pun ceva de genul if(DamageType.CompareTag( "Melee"/"Projectile"/"Magic")) si dupa sa verific de care damage Melee sau celelalte este.
            //Exemplu: Daca pDamage.DT == "Magic" atunci mai fac un if in care verific tipul de magie, gen "fire", "ice", etc.
            if(pDamage.DamageType == "Meele"){
                pDamage.TakeDamage(20);
            }
            //pDamage.TakeDamage(20);

        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            if(eDamage.DamageType == "Meele"){
                eDamage.TakeDamage(20);
            }
            //eDamage.TakeDamage(20);
        }
    }
}
