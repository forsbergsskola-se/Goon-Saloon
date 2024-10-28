using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public EnemyController[] enemies;

        [Header("Spawn Settings")]
        public float initialSpawnDelay;
        public float spawnRate;
        public float spawnRateAcceleration;

        private void Start()
        {
            StartCoroutine(EnemySpawner());
        }

        private IEnumerator EnemySpawner()
        {
            yield return new WaitForSeconds(initialSpawnDelay);
            
            while (true)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnRate);

                spawnRate -= spawnRate * spawnRateAcceleration;
            }

            
        }
        

        private void SpawnEnemy()
        {
            List<EnemyController> inactiveEnemies = new List<EnemyController>();
            inactiveEnemies.AddRange(enemies.Where(enemy => !enemy.gameObject.activeSelf));
            
            if(inactiveEnemies.Count == 0)
                return;

            int index = Random.Range(0, inactiveEnemies.Count);
            inactiveEnemies[index].SetActive(true);
        }
    }
}