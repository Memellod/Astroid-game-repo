using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

namespace GameLogic
{
    public class AsteroidSpawner : MonoBehaviour
    {
        GameManager gameManager;
        [SerializeField] float minRadius, maxRadius;
        [SerializeField] GameObject[] asteroids;
        [SerializeField] Transform[] spawnPos;
        [SerializeField] float spawnDelay = 0.8f;
        List<GameObject> asteroidList = new List<GameObject>();

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            StartCoroutine("Spawn");
        }

        IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);
                if (gameManager.isPlaying && asteroidList.Count < 10)
                {
                    SpawnAsteroid();
                }
            }
        }

        private void SpawnAsteroid()
        {
            int asteroidIndex = UnityEngine.Random.Range(0, asteroids.Length);
            Vector3 rngPos = UnityEngine.Random.insideUnitCircle * (maxRadius - minRadius);
            Vector3 position = rngPos + rngPos.normalized * minRadius;
            asteroidList.Add(Instantiate(asteroids[asteroidIndex], position, Quaternion.identity));
        }

        public void DestroyAsteroid(GameObject go)
        {
            asteroidList.Remove(go);
            Destroy(go);
        }

        public void DestroyAllAsteroids()
        {
            asteroidList.ForEach(x => Destroy(x));
            asteroidList.Clear();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Vector3.zero, minRadius);
            Gizmos.DrawWireSphere(Vector3.zero, maxRadius);
        }
    }
}