using GameLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    [SerializeField] float maxFlySpeed = 1f;

    [SerializeField] float flyVelocity = 1f;
    [SerializeField] float rotateSpeed = 2.0f;
    [SerializeField] Transform ship;
    [SerializeField] float reloadTime = 0.1f;

    GameManager gameManager;
    bool isReloading = false;
    Rigidbody rb;
    private ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isPlaying)
        {
            Move();
            Fire();
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isReloading)
            {
                LaunchProjectile();
            }
        }

    }

    private void LaunchProjectile()
    {
        particleSystem.Emit(1);
        isReloading = true;
        StartCoroutine("Reload");
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity += flyVelocity * ship.transform.forward * Time.deltaTime;
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity += -flyVelocity * ship.transform.forward * Time.deltaTime;
        }
        else
        {
            rb.velocity -= rb.velocity * 0.8f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            ship.Rotate(new Vector3(0, 1, 0), -rotateSpeed * Time.deltaTime);
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            ship.Rotate(new Vector3(0, 1, 0), rotateSpeed * Time.deltaTime);
        }



        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxFlySpeed);
    }
}
