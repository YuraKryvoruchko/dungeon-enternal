using UnityEngine;
using UnityEngine.AI;
using DungeonEternal.Weapons;

namespace DungeonEternal.AI
{
    [RequireComponent(typeof(BulletEjector), typeof(EnemyVision))]
    public class BigDevil : Enemy, IEjection
    {
        [Space]
        [Tooltip("Shooting properties")]
        [SerializeField] private Transform _bulletPoint;

        [SerializeField] private Bullet _bulletPrefabs;

        [Tooltip("Animator properties")]
        [SerializeField] private string _nameAttackParameter = "onAttack";
        [SerializeField] private string _nameWalkParameter = "onWalk";
        [Space]
        [SerializeField] private float _extremeDistance;

        private EnemyVision _enemyVision;

        private Animator _animator;

        private BulletEjector _bulletEjector;

        private void Awake()
        {
            NavAgent = GetComponent<NavMeshAgent>();
            NavAgent.speed = Speed;
            NavAgent.stoppingDistance = MinDistance;

            EnemyRigidbody = GetComponent<Rigidbody>();

            _bulletEjector = GetComponent<BulletEjector>();

            _enemyVision = GetComponent<EnemyVision>();
            _animator = GetComponent<Animator>();
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

            if (PlayerCurrent != null)
                MoveToEnemy();
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


            transform.LookAt(new Vector3(PlayerCurrent.transform.position.x, transform.position.y, 
                PlayerCurrent.transform.position.z));
        }

        protected override void Attack()
        {
            Vector3 direction = PlayerCurrent.transform.position - _bulletPoint.position;

            _bulletEjector.EnjectFromPool(_bulletPrefabs, _bulletPoint.position, direction);
        }

        private void DepartureFromEnemy()
        {
            NavAgent.Move(Vector3.back * Speed * Time.deltaTime);
        }

        public void Eject(Vector3 direction, float energy)
        {
            EnemyRigidbody.AddForce(direction * energy, ForceMode.VelocityChange);
        }
    }
}
