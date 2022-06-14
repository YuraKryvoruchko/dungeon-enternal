using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DungeonEternal.Weapons;

namespace DungeonEternal.AI
{
    [RequireComponent(typeof(EnemyVision))]
    public class Boss : Enemy, IEjection
    {
        [Space]
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _extremeDistance;

        [SerializeField] private Firearms[] _weaponsInHands;

        [Tooltip("Animator properties")]
        [SerializeField] private string _nameAttackParameter = "onAttack";
        [SerializeField] private string _nameWalkParameter = "onWalk";

        public static event Action<Boss> Death;
        public override event Action OnDead;

        private EnemyVision _enemyVision;

        private Animator _animator;

        private IEnumerator _attackCorutine;

        private void Awake()
        {
            NavAgent = GetComponent<NavMeshAgent>();
            NavAgent.speed = Speed;
            NavAgent.stoppingDistance = MinDistance;

            EnemyRigidbody = GetComponent<Rigidbody>();

            _animator = GetComponent<Animator>();
            _enemyVision = GetComponent<EnemyVision>();

            _attackCorutine = Shoot();
        }

        protected override void OnEnableObject()
        {
            _enemyVision.EnemyDiscovered += () => TargetDetected = true;
            _enemyVision.EnemyEscape += () => TargetDetected = false;

            for (int i = 0; i < PartsOfTheBody.Length; i++)
                PartsOfTheBody[i].OnEjection += Eject;
        }
        protected override void OnDisableObject()
        {
            _enemyVision.EnemyDiscovered -= () => TargetDetected = true;
            _enemyVision.EnemyEscape -= () => TargetDetected = false;

            for (int i = 0; i < PartsOfTheBody.Length; i++)
                PartsOfTheBody[i].OnEjection -= Eject;
        }

        protected override void OnStart()
        {
            FindEnemy();
        }

        protected override void OnUpdate()
        {
            if (CanAttack() == true)
            {
                _animator.SetTrigger(_nameAttackParameter);
            }
            else
            {
                _animator.SetTrigger(_nameWalkParameter);

                float distance = GetDistance(PlayerCurrent.transform.position);

                if (distance >= _extremeDistance && distance >= MinDistance)
                    MoveToEnemy();
                else if (distance >= _extremeDistance && distance < MinDistance)
                    Stay();
                else
                    DepartureFromEnemy();
            }

            LookEnemy();
        }
        protected override void Attack()
        {
            for(int i = 0; i < _weaponsInHands.Length; i++)
                _weaponsInHands[i].Attack(PlayerCurrent.transform);
        }

        public override void TakeDamage(float damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Death?.Invoke(this);
                OnDead?.Invoke();

                Death = null;
                OnDead = null;

                Destroy(gameObject);
            }
        }

        private void LookEnemy()
        {
            Vector3 relativePos = PlayerCurrent.transform.position - transform.position;

            Vector3 direction = new Vector3(relativePos.x, 0, relativePos.z);

            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 
                _rotationSpeed * Time.deltaTime);
        }
        private void DepartureFromEnemy()
        {
            NavAgent.Move(Vector3.back * Speed * Time.deltaTime);
        }
        private IEnumerator Shoot()
        {
            while (true)
            {
                yield return new WaitUntil(() => CanAttack());

                _animator.SetTrigger(_nameAttackParameter);

                Attack();

                yield return new WaitForSeconds(FireRate);
            }
        }

        public void Eject(Vector3 direction, float energy)
        {
            EnemyRigidbody.AddForce(direction * energy, ForceMode.VelocityChange);
        }
    }
}
