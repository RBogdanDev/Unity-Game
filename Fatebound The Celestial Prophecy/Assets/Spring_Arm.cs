using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring_Arm : MonoBehaviour
{
    public float fallow_speed;
    public Transform target;

    void Update()
    {
        Vector3 newpos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newpos, fallow_speed * Time.deltaTime);
    }
}
