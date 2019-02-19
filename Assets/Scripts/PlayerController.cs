﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public Transform gunPivot;
    public Transform gun;

    public Transform bullet;

    private Rigidbody2D rb2d;
    
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    void Update() {
        // rotate gun
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDelta = mouseWorld - gunPivot.position;
        float angle = Mathf.Atan2(mouseDelta.y, mouseDelta.x) * 180 / Mathf.PI;
        gunPivot.rotation = Quaternion.Euler(0, 0, angle);

        if(Input.GetButtonDown("Fire1")) {
            Instantiate(bullet, gun.position, gun.rotation);
        }
    }

    void FixedUpdate()
    {
        float hSpeed = Input.GetAxis("Horizontal") * maxSpeed;
        float vSpeed = Input.GetAxis("Vertical") * maxSpeed;
        rb2d.velocity = new Vector2(hSpeed, vSpeed);
    }
}
