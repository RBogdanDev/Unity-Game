using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring_Arm : MonoBehaviour
{
    private float fallow_speed = 1.5f;
    public Transform target;

    void Update()
    {
        if (target != null)
        {
            Vector3 newpos = new Vector3(target.position.x, target.position.y, -10f);
            transform.position = Vector3.Slerp(transform.position, newpos, fallow_speed * Time.deltaTime);
        }
    }
}
