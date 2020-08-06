using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


    public class ProjectileDestroysAsteroid
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ProjectileDestroysAsteroidSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ProjectileDestroysAsteroidWithEnumeratorPasses()
        {
            GameObject asteroid = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Asteroid 1"), Vector3.zero, Quaternion.identity);
            yield return null;
        }
    }
