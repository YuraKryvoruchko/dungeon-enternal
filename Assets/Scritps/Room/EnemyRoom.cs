using System;
using UnityEngine;
using DungeonEternal.AI;
using DungeonEternal.Player;
using DungeonEternal.Audio;

namespace DungeonEternal.Rooms
{
    public class EnemyRoom : MonoBehaviour
    {
        [SerializeField] private Room—ondition _roomCondition;

        [SerializeField] private Door[] _doors;

        private Spawner _spawner;

        private EnemyCounter _enemyCounter = new EnemyCounter();

        public static event Action RoomWasCleaned;

        private void OnEnable()
        {
            _enemyCounter.AllEnemiesDead += CheckingEmptyRooms;
        }
        private void OnDisable()
        {
            _enemyCounter.AllEnemiesDead -= CheckingEmptyRooms;
        }

        private void Awake()
        {
            _spawner = GetComponentInChildren<Spawner>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out HandsPlayer handsPlayer))
            {
                if (_roomCondition == Room—ondition.Filled)
                    Spawn();
                else if (_roomCondition == Room—ondition.InBattle)
                    Debug.Log("Room in battle");
                else if (_roomCondition == Room—ondition.Empty)
                    Debug.Log("Room is empty");
            }
        }

        private void Spawn()
        {
            if (_spawner != null)
            {
                Enemy[] arrayEnemys = _spawner.Spawn().ToArray();

                _enemyCounter.StartCountEnemy(arrayEnemys);

                _roomCondition = Room—ondition.InBattle;

                for (int i = 0; i < _doors.Length; i++)
                    _doors[i].CloseDoor();

                Debug.Log("Room to battle");
            }
            else
            {
                Debug.LogWarning("Spawner is null!");
            }
        }
        private void CheckingEmptyRooms() 
        {
            _roomCondition = Room—ondition.Empty;

            for (int i = 0; i < _doors.Length; i++)
                _doors[i].OpenDoor();

            RoomWasCleaned?.Invoke();
        }
    }

    public enum Room—ondition
    {
        Filled,
        Empty,
        InBattle
    }
}