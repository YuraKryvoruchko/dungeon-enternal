using UnityEngine;
using UnityEngine.AI;
using DungeonEternal.Weapons;

namespace DungeonEternal.AI
{
    [RequireComponent(typeof(EnemyVision), typeof(Animator), typeof(NavMeshAgent))]
    public class SmallDevil : Enemy, IEjection
    {
        [Tooltip("Animator properties")]
        [SerializeField] private string _nameAttackParameter = "onAttack";
        [SerializeField] private string _nameBlockFollowParameter = "blockFollow";
        [SerializeField] private string _nameDeadParameter = "onDead";

        private Animator _animator;

        [SerializeField] private float _extremeDistance;

        private EnemyVision _enemyVision;

        protected override void OnAwake()
        {
            _animator = GetComponent<Animator>();

            _enemyVision = GetComponent<EnemyVision>();
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
                _animator.SetBool(_nameBlockFollowParameter, false);

                float distance = GetDistance(PlayerCurrent.transform.position);

                if (distance >= _extremeDistance && distance >= MinDistance)
                    MoveToEnemy();
                else if (distance <= _extremeDistance)
                    DepartureFromEnemy();

                transform.LookAt(new Vector3(PlayerCurrent.transform.position.x, transform.position.y, 
                    PlayerCurrent.transform.position.z));
            }
        }
        protected override void Attack()
        {
            PlayerCurrent.TakeDamage(Damage);
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
