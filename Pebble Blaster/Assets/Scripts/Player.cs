using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]//force the playercontroller script onto whatever this script is on
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{

    public float moveSpeed = 5;

    public Crosshairs crosshairs;

    PlayerController controller;
    GunController gunController;
    Camera viewCamera;

    protected override void Start()
    {
        base.Start();      
    }

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        viewCamera = Camera.main;
        gunController = GetComponent<GunController>();
        FindObjectOfType<Spawner>().OnNewWave += OnNewWave;
    }

    void OnNewWave(int waveNumber)
    {
        health = startingHealth;
        gunController.EquipGun(waveNumber - 1);

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
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * transform.position.y);
        float rayDistance;

        if(groundPlane.Raycast(ray, out rayDistance))//will return true if it intersepts with the ground plane
        {
            Vector3 point = ray.GetPoint(rayDistance);//returns the point that the ray has hit the plane
            //Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
            crosshairs.transform.position = point;
            crosshairs.DetectTargets(ray);
            //sqr magnitude faster than normal magnitude
            if ((new Vector2(point.x, point.z) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 2)
                {
                gunController.Aim(point);
            }
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

        if(Input.GetKeyDown(KeyCode.R))
        {
            gunController.Reload();
        }
    }

    public override void Die()
    {
        AudioManager.instance.PlaySound("Player Death", transform.position);
        base.Die();
    }
}
