using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidStats : MonoBehaviour
{

    private int points = 0;
    Rigidbody rb;

    Vector3 velocity;
    Vector3 angularVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        points = Random.Range(20, 40);
        transform.localScale = Vector3.one * points / 100f;
        float tumble = Random.Range(750, 2500);        
        angularVelocity = Random.insideUnitSphere * tumble;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        velocity = (player.transform.position - transform.position).normalized * Random.Range(1, 2.5f);
    }

    public int GetPoints()
    {
        return points;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity;
        rb.angularVelocity = angularVelocity;
    }
}
