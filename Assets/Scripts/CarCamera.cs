using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{
    public Transform target;

    public float rotSpeed = 10f;
    public float movSpeed = 10f;

    Vector3 refe;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * movSpeed);
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.fixedDeltaTime * rotSpeed);
    }
}
