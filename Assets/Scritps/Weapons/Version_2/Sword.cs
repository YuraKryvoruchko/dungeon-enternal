using System;
using System.Collections;
using UnityEngine;
using DungeonEternal.AI;

namespace DungeonEternal.Weapons
{
    public class Sword : СoldSteel
    {
        [Header("Sword properties")]
        [SerializeField] private float _damage;
        [SerializeField] private float _maxDistace = 3f;
        [Tooltip("Attack animation duration")]
        [SerializeField] private float _speedAttack;
        [SerializeField] private float _shockForce;

        [Header("Audio properties")]
        [SerializeField] private AudioSource _audioSource;

        [SerializeField] private AudioClip _audioAttack;

        [Header("Animator properties")]
        [SerializeField] private Animator _animator;
        [SerializeField] private string _nameAttackParameter = "Attack";

        public override event Action OnAttack;

        public override void Attack()
        {
            if (StateSteel == StateСoldSteel.None)
            {
                StateSteel = StateСoldSteel.Attack;

                _audioSource.PlayOneShot(_audioAttack);
                _animator.SetTrigger(_nameAttackParameter);

                StartCoroutine(Timer());

                Enemy enemy = GetEnemy();

                if (enemy != null)
                {
                    if(enemy.TryGetComponent(out IHealth health))
                        InflictDamage(health);

                    Ray ray = ShootAndGetRay();
                    ToPush(enemy.transform, ray.direction);
                }

                OnAttack?.Invoke();
            }
        }
        public override void Attack(Transform target) { }

        private void ToPush(Transform enemy, Vector3 rayDirection)
        {
            Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();

            enemyRigidbody.AddForce(rayDirection * _shockForce, ForceMode.Impulse);
        }
        private void InflictDamage(IHealth health)
        {
            Debug.Log(health.GetType());

            health.TakeDamage(_damage);
        }
        private Enemy GetEnemy()
        {
            Ray ray = ShootAndGetRay();

            if (Physics.Raycast(ray, out RaycastHit hit, _maxDistace, 1, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.TryGetComponent(out Enemy enemy))
                    return enemy;
                else
                    return default;
            }
            else
            {
                Debug.LogWarning("Ray did not collide with any object");

                return default;
            }
        }
        private Ray ShootAndGetRay()
        {
            Vector3 starPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = Camera.main.ScreenPointToRay(starPosition);

            Debug.DrawRay(ray.origin, ray.direction, Color.red, 100f);

            return ray;
        }
        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_speedAttack);

            StateSteel = StateСoldSteel.None;
        }
    }
}
