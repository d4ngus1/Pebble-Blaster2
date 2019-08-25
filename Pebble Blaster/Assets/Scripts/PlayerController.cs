using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Vector3 velocity;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 moveVelocity)
    {
        velocity = moveVelocity;
    }

    private void FixedUpdate()//calls update multiple times per frame
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    public void LookAt(Vector3 point)
    {
        Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
        transform.LookAt(heightCorrectedPoint);
    }
}
