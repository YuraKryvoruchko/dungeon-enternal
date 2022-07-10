using System;
using UnityEngine;
using DungeonEternal.Weapons;

namespace DungeonEternal.AI
{
    [RequireComponent(typeof(EnemyVision))]
    public class Boss : Enemy, IEjection
    {
        [Space]
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _extremeDistance;

        [Tooltip("Weapon properties")]
        [SerializeField] private Firearms[] _weaponsInHands;

        [Tooltip("Animator properties")]
        [SerializeField] private string _nameAttackParameter = "onAttack";
        [SerializeField] private string _nameBlockFollowParameter = "blockFollow";
        [SerializeField] private string _nameDeadParameter = "onDead";

        public static event Action<Boss> Death;

        private EnemyVision _enemyVision;

        private Animator _animator;

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
                else if(distance <= _extremeDistance)
                    DepartureFromEnemy();
            }

            LookEnemy();
        }
        protected override void Attack()
        {
            for(int i = 0; i < _weaponsInHands.Length; i++)
                _weaponsInHands[i].Attack(PlayerCurrent.transform);
        }

        private void LookEnemy()
        {
            Vector3 relativePos = PlayerCurrent.transform.position - transform.position;

            Vector3 direction = new Vector3(relativePos.x, 0, relativePos.z);

            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 
                _rotationSpeed * Time.deltaTime);
        }
        private void Die()
        {
            Death?.Invoke(this);
            Death = null;

            _animator.SetTrigger(_nameDeadParameter);

            Debug.Log(gameObject.name + " dead");
        }

        public void Eject(Vector3 direction, float energy)
        {
            EnemyRigidbody.AddForce(direction * energy, ForceMode.VelocityChange);
        }
    }
}
