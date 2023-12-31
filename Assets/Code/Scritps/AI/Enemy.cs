using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DungeonEternal.Player;
using DungeonEternal.Health;

using Random = UnityEngine.Random;

namespace DungeonEternal.AI
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(Rigidbody))]
    public abstract class Enemy : MonoBehaviour, IHealth
    {
        [SerializeField] private float _health;
        [SerializeField] private float _minDistance;
        [Space]
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _fireRate;
        [Space]
        [SerializeField] private float _timeToChangePath = 2f;
        [SerializeField] private Vector3 _offset;
        [Space]
        [SerializeField] private PartOfTheBody[] _partsOfTheBody;

        private bool _targetDetected = false;
        private bool _canChangePath = true;

        private HealthPlayer _playerCurrent;

        private Rigidbody _rigidbody;

        private NavMeshAgent _navMeshAgent;

        private float _distanceToPlayer;

        protected float Health { get => _health; set => _health = value; }

        protected float MinDistance { get => _minDistance; set => _minDistance = value; }

        protected float Speed { get => _speed; set => _speed = value; }
        protected float Damage { get => _damage; set => _damage = value; }
        protected float FireRate { get => _fireRate; set => _fireRate = value; }

        protected bool TargetDetected { get => _targetDetected; set => _targetDetected = value; }

        protected HealthPlayer PlayerCurrent { get => _playerCurrent; set => _playerCurrent = value; }

        protected Rigidbody EnemyRigidbody { get => _rigidbody; set => _rigidbody = value; }

        protected NavMeshAgent NavAgent { get => _navMeshAgent; set => _navMeshAgent = value; }

        protected Vector3 Offset { get => _offset; set => _offset = value; }

        protected PartOfTheBody[] PartsOfTheBody { get => _partsOfTheBody; set => _partsOfTheBody = value; }

        public virtual event Action OnDead;

        private void Awake()
        {
            NavAgent = GetComponent<NavMeshAgent>();
            NavAgent.speed = Speed;
            NavAgent.stoppingDistance = MinDistance;

            EnemyRigidbody = GetComponent<Rigidbody>();

            OnAwake();
        }
        private void OnEnable()
        {
            for (int i = 0; i < PartsOfTheBody.Length; i++)
                PartsOfTheBody[i].OnTakeDamage += TakeDamage;

            OnEnableObject();
        }
        private void OnDisable()
        {
            for (int i = 0; i < PartsOfTheBody.Length; i++)
                PartsOfTheBody[i].OnTakeDamage -= TakeDamage;

            OnDisableObject();
        }
        
        private void Start()
        {
            OnStart();
        }
        private void Update()
        {
            OnUpdate();
        }

        public virtual void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                OnDead?.Invoke();

                OnDead = null;
            }
        }

        protected void FindEnemy()
        {
            PlayerCurrent = FindObjectOfType<HealthPlayer>();
        }
        protected bool CanAttack()
        {
            if (PlayerCurrent == null)
            {
                Debug.LogWarning("Player is null!");

                return false;
            }

            float distance = GetDistance(_playerCurrent.transform.position);

            if (distance <= MinDistance && TargetDetected == true)
                return true;
            else
                return false;
        }
        protected float GetDistance(Vector3 point)
        {
            _distanceToPlayer = Vector3.Distance(NavAgent.transform.position, point);

            return _distanceToPlayer;
        }

        protected virtual void MoveToEnemy()
        {
            if (_canChangePath == false)
                return;

            StartCoroutine(RunTimerToChangePath());

            Vector3 point = GetPointMove();

            NavAgent.SetDestination(point);
        }
        protected virtual void DepartureFromEnemy()
        {
            NavAgent.Move(Vector3.back * Speed * Time.deltaTime);
        }

        private Vector3 GetPointMove()
        {
            Vector3 playerTransform = new Vector3(PlayerCurrent.transform.position.x, 0,
                PlayerCurrent.transform.position.z);

            float offsetX = Random.value > 0.5f ? Offset.x : -Offset.x;
            float offsetY = Random.value > 0.5f ? Offset.y : -Offset.y;
            float offsetZ = Random.value > 0.5f ? Offset.z : -Offset.z;

            Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);

            Vector3 point = playerTransform + offset;

            return point;
        }
        private IEnumerator RunTimerToChangePath()
        {
            _canChangePath = false;

            yield return new WaitForSeconds(_timeToChangePath);

            _canChangePath = true;
        }

        protected abstract void Attack();

        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnAwake() { }
        protected virtual void OnEnableObject() { }
        protected virtual void OnDisableObject() { }
    }
}

