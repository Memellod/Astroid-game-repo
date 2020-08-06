using GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_handler : MonoBehaviour
{
    AsteroidSpawner asteroidSpawner;
    ParticleSystem ps;
    GameManager gameManager;


    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(4);
        gameManager.GameOver();
    }

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        gameManager = FindObjectOfType<GameManager>();
        asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.CompareTag("asteroid"))
        {
            gameManager.AddPoints(other.GetComponent<asteroidStats>().GetPoints());
            asteroidSpawner.DestroyAsteroid(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (transform.root.CompareTag("Player") && collision.transform.CompareTag("asteroid"))
        {
            StartCoroutine("GameOver");
            gameManager.isPlaying = false;
            ps.Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}

