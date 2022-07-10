using UnityEngine;
using DungeonEternal.Player;
using DungeonEternal.AI;
using DungeonEternal.Audio;

namespace DungeonEternal.Rooms
{
    public class BossRoom : MonoBehaviour
    {
        [SerializeField] private int _numberOfEmptyRoomsForOpeningARoom = 5;

        [SerializeField] private Room—ondition _roomCondition = Room—ondition.Filled;

        [SerializeField] private Door[] _doors;

        [SerializeField] private AudioClip _inBattleMusic;
        [SerializeField] private AudioClip _afterBattleMusic;

        private static BackgroundMusic s_backgroundMusic;

        private Spawner _spawner;

        private EnemyCounter _enemyCounter = new EnemyCounter();

        private int _numberEmptyRooms = 0;

        private void OnEnable()
        {
            EnemyRoom.RoomWasCleaned += CountingEmptyRooms;

            _enemyCounter.AllEnemiesDead += OpenRoom;
        }
        private void OnDisable()
        {
            EnemyRoom.RoomWasCleaned -= CountingEmptyRooms;

            _enemyCounter.AllEnemiesDead -= OpenRoom;
        }

        private void Awake()
        {
            _spawner = GetComponentInChildren<Spawner>();
        }

        private void Start()
        {
            if (s_backgroundMusic == null)
                s_backgroundMusic = FindObjectOfType<BackgroundMusic>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HandsPlayer handsPlayer))
                DisableDoors();
        }

        private void CountingEmptyRooms()
        {
            _numberEmptyRooms++;
        }
        private void DisableDoors()
        {
            if(_numberEmptyRooms >= _numberOfEmptyRoomsForOpeningARoom && _roomCondition == Room—ondition.Filled)
            {
                for(int i = 0; i < _doors.Length; i++)
                    _doors[i].OpenDoor();

                Enemy[] enemies = _spawner.Spawn().ToArray();

                _enemyCounter.StartCountEnemy(enemies);

                if(s_backgroundMusic != null && _inBattleMusic != null)
                    StartCoroutine(s_backgroundMusic.SetMusic(_inBattleMusic));

                _roomCondition = Room—ondition.InBattle;
            }
        }
        private void OpenRoom()
        {
            if (s_backgroundMusic != null && _afterBattleMusic != null)
                StartCoroutine(s_backgroundMusic.SetMusic(_afterBattleMusic));

            OpenDoors();

            _roomCondition = Room—ondition.Empty;
        }
        private void OpenDoors()
        {
            for (int i = 0; i < _doors.Length; i++)
                _doors[i].OpenDoor();
        }
    }
}
