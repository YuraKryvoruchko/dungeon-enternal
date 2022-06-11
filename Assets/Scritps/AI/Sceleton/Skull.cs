using System.Collections;
using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;

namespace DungeonEternal.AI
{
    [RequireComponent(typeof(EnemyVision), typeof(Animator), typeof(NavMeshAgent))]
    public class Skull : Enemy
    {
        [Space]
        [SerializeField] private float _angleSpeed = 20f;
        [SerializeField] private float _timeToChangeAxit = 3f;
        [SerializeField] private float _extremeDistance;

        [SerializeField] private Vector3[] _sideTurns = new Vector3[2] { Vector3.up, Vector3.down };

        [Tooltip("Animator properties")]
        [SerializeField] private string _nameAttackParameter = "onAttack";
        [SerializeField] private string _nameWalkParameter = "onWalk";

        private Animator _animator;

        private Vector3 _axis = Vector3.up;

        private bool _timerWork = false;

        private EnemyVision _enemyVision;

        private IEnumerator _timerToChangeAxit;

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.speed = Speed;
            NavMeshAgent.stoppingDistance = MinDistance;

            _animator = GetComponent<Animator>();

            _enemyVision = GetComponent<EnemyVision>();
        }
        protected override void OnEnableObject()
        {
            _enemyVision.EnemyDiscovered += () => TargetDetected = true;
            _enemyVision.EnemyEscape += () => TargetDetected = false;
        }
        protected override void OnDisableObject()
        {
            _enemyVision.EnemyDiscovered -= () => TargetDetected = true;
            _enemyVision.EnemyEscape -= () => TargetDetected = false;
        }

        protected override void OnStart()
        {
            FindEnemy();

            if (PlayerCurrent != null)
                MoveToEnemy();

            _timerToChangeAxit = TimerToChangeAxit();

            StartCoroutine(_timerToChangeAxit);
        }
        protected override void OnUpdate()
        {
            if(CanAttack() == true)
            {
                Dodging();

                _animator.SetTrigger(_nameAttackParameter);

                _timerWork = true;
            }
            else
            {
                _timerWork = false;

                _animator.SetTrigger(_nameWalkParameter);

                float distance = GetDistance(PlayerCurrent.transform.position);

                if (distance >= _extremeDistance && distance >= MinDistance)
                    MoveToEnemy();
                else if (distance >= _extremeDistance && distance < MinDistance)
                    Stay();
                else
                    DepartureFromEnemy();
            }

            transform.LookAt(PlayerCurrent.transform);
        }

        protected override void Attack()
        {
            PlayerCurrent.TakeDamage(Damage);
        }

        private void Dodging()
        {
            NavMeshAgent.transform.RotateAround(PlayerCurrent.transform.position, _axis,
                _angleSpeed * Time.deltaTime);
        }
        private void DepartureFromEnemy()
        {
            NavMeshAgent.Move(Vector3.back * Speed * Time.deltaTime);
        }
        private IEnumerator TimerToChangeAxit()
        {
            while (true)
            {
                yield return new WaitUntil( () => _timerWork == true);

                int number = Random.Range(0, _sideTurns.Length);

                if (number == _sideTurns.Length)
                    number = _sideTurns.Length - 1;

                _axis = _sideTurns[number];

                yield return new WaitForSeconds(_timeToChangeAxit);
            }
        }
        private IEnumerator AttackCorutine()
        {
            while (true)
            {
                yield return new WaitUntil(() => CanAttack());

                Attack();

                yield return new WaitForSeconds(FireRate);
            }
        }
    }
}
