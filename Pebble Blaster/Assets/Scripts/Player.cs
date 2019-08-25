using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]//force the playercontroller script onto whatever this script is on
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{

    public float moveSpeed = 5;

    PlayerController controller;
    GunController gunController;
    Camera viewCamera;

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        viewCamera = Camera.main;
        gunController = GetComponent<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        //look input
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if(groundPlane.Raycast(ray, out rayDistance))//will return true if it intersepts with the ground plane
        {
            Vector3 point = ray.GetPoint(rayDistance);//returns the point that the ray has hit the plane
            //Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
        }

        //weapon input
        if(Input.GetMouseButton(0))
        {
            gunController.OnTriggerHold();
        }

        if(Input.GetMouseButtonUp(0))
        {
            gunController.OnTriggerRelease();
        }
    }
}
