using UnityEngine;
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
        [SerializeField] private string _nameBlockFollowParameter = "blockFollow";
        [SerializeField] private string _nameDeadParameter = "onDead";
        [Space]
        [SerializeField] private float _extremeDistance;

        private EnemyVision _enemyVision;

        private Animator _animator;

        private BulletEjector _bulletEjector;

        protected override void OnAwake()
        {
            _bulletEjector = GetComponent<BulletEjector>();

            _enemyVision = GetComponent<EnemyVision>();
            _animator = GetComponent<Animator>();
        }
        protected override void OnEnableObject()
        {
            _enemyVision.EnemyDiscovered += () => TargetDetected = true;
            _enemyVision.EnemyEscape += () => TargetDetected = false;

            this.OnDead += Die;

            for (int i = 0; i < PartsOfTheBody.Length; i++)
                PartsOfTheBody[i].OnEjection += Eject;
        }
        protected override void OnDisableObject()
        {
            _enemyVision.EnemyDiscovered -= () => TargetDetected = true;
            _enemyVision.EnemyEscape -= () => TargetDetected = false;

            this.OnDead -= Die;

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
                _animator.SetBool(_nameBlockFollowParameter, true);
            }
            else
            {
                float distance = GetDistance(PlayerCurrent.transform.position);

                if (distance >= _extremeDistance && distance >= MinDistance)
                    MoveToEnemy();
                else if (distance <= _extremeDistance)
                    DepartureFromEnemy();

                _animator.SetBool(_nameBlockFollowParameter, false);
            }


            transform.LookAt(new Vector3(PlayerCurrent.transform.position.x, transform.position.y, 
                PlayerCurrent.transform.position.z));
        }

        protected override void Attack()
        {
            Vector3 direction = PlayerCurrent.transform.position - _bulletPoint.position;

            _bulletEjector.EnjectFromPool(_bulletPrefabs, _bulletPoint.position, direction);
        }

        private void Die()
        {
            _animator.SetTrigger(_nameDeadParameter);

            Debug.Log(gameObject.name + " dead");
        }

        public void Eject(Vector3 direction, float energy)
        {
            EnemyRigidbody.AddForce(direction * energy, ForceMode.VelocityChange);
        }
    }
}
