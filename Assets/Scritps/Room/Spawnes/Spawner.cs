using System.Collections.Generic;
using UnityEngine;
using DungeonEternal.AI;

using Random = UnityEngine.Random;

namespace DungeonEternal.Rooms
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int _minNumberEnemies = 2;
        [SerializeField] private int _maxNumberEnemies = 3;

        [SerializeField] private Enemy[] _enemyPrefab;

        [Space]
        [SerializeField] private BoxCollider _spawnPoint;
        [Space]
        [SerializeField] private List<Enemy> _spawnedEnemies;

        public List<Enemy> Spawn()
        {
            int randomNumberOfEnemies = Random.Range(_minNumberEnemies, _maxNumberEnemies);

            for (int i = 0; i < randomNumberOfEnemies; i++)
            {
                int randomEnemy = Random.Range(0, _enemyPrefab.Length);
                
                float randomXPosition = Random.Range(_spawnPoint.transform.position.x - (_spawnPoint.size.x / 2), 
                    _spawnPoint.transform.position.x + (_spawnPoint.size.x / 2));
                float randomZPosition = Random.Range(_spawnPoint.transform.position.z - (_spawnPoint.size.z / 2),
                    _spawnPoint.transform.position.z + (_spawnPoint.size.z / 2));

                Enemy newEnemy = Instantiate(_enemyPrefab[randomEnemy], 
                    new Vector3(randomXPosition, 0, randomZPosition), Quaternion.identity);

                _spawnedEnemies.Add(newEnemy);
            }

            return _spawnedEnemies;
        }
    }
}