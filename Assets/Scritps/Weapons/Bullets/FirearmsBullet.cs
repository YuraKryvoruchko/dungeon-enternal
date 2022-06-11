using System;
using UnityEngine;

namespace DungeonEternal.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class FirearmsBullet : Bullet, IPooledObejct
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;

        private const float _timeToDestroy = 5;

        public override event Action OnHitOrDestroy;

        private void OnEnable()
        {
            StartCoroutine(StartTheCountdownToDestory(_timeToDestroy));

            OnCreate(Vector3.zero, transform.forward);
        }

        private void Update()
        {
            transform.position += _speed * Time.deltaTime * Direction;
        }

        public override void OnCreate(Vector3 position, Vector3 direction)
        {
            transform.position = position;

            Direction = direction.normalized;

            transform.localRotation = Quaternion.LookRotation(Direction, Vector3.up);
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
