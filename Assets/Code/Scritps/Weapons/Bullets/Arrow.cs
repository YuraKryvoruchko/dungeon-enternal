using System;
using UnityEngine;

namespace DungeonEternal.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Arrow : Bullet
    {
        [SerializeField] private float _damagePerUnitOfEnergy = 0.6f;

        private float _damage = 0f;

        private Rigidbody _rigidbody;

        public override event Action OnHitOrDestroy;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;

            OnCreate(Vector3.zero, transform.forward);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Bullet>())
                return;

            if (collision.gameObject.TryGetComponent(out IHealth health))
                InflictDamage(health);

            OnHitOrDestroy?.Invoke();

            Debug.Log("Hit");
        }

        public override void OnCreate(Vector3 position, Vector3 direction)
        {
            transform.position = position;

            SetDirection(direction);
        }

        public void ToPush(float energy)
        {
            _rigidbody.isKinematic = false;

            StartCoroutine(StartTheCountdownToDestory(5));

            _damage = energy * _damagePerUnitOfEnergy;

            _rigidbody.AddForce(transform.forward * energy, ForceMode.Impulse);
        }
        public void SetDirection(Vector3 direction)
        {
            Direction = transform.TransformDirection(direction);
        }

        private void InflictDamage(IHealth health)
        {
            Debug.Log(health.GetType());

            health.TakeDamage(_damage);
        }

        protected override void DestoyBullet()
        {
            OnHitOrDestroy?.Invoke();
        }
    }
}
