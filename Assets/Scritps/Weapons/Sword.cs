using System;
using System.Collections;
using UnityEngine;

namespace DungeonEternal.Weapons
{
    public interface IEjection
    {
        public void Eject(Vector3 direction, float energy);
    }

    public class Sword : СoldSteel
    {
        [Header("Sword properties")]
        [SerializeField] private float _damage;
        [SerializeField] private float _maxDistace = 3f;
        [SerializeField] private float _ejectEnergy;
        [Tooltip("Attack animation duration")]
        [SerializeField] private float _speedAttack;

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

                Ray ray = ShootAndGetRay();

                if (Physics.Raycast(ray, out RaycastHit hit, _maxDistace, 1, QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.TryGetComponent(out IHealth health))
                        InflictDamage(health);

                    if (hit.collider.TryGetComponent(out IEjection еjectObject))
                        еjectObject.Eject(ray.direction.normalized, _ejectEnergy);
                }

                OnAttack?.Invoke();
            }
        }
        public override void Attack(Transform target) { }

        private void InflictDamage(IHealth health)
        {
            Debug.Log(health.GetType());

            health.TakeDamage(_damage);
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
