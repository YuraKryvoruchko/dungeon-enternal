using System;
using System.Collections;
using UnityEngine;

namespace DungeonEternal.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Bullet : MonoBehaviour, IPooledObejct
    {
        [SerializeField] private const float _timeToDestroy = 5;

        [SerializeField] private BulletType _type;

        private Vector3 _direction = Vector3.forward;

        public abstract event Action OnHitOrDestroy;

        public BulletType Type { get => _type; }
        protected Vector3 Direction { get => _direction; set => _direction = value; }

        public abstract void OnCreate(Vector3 position, Vector3 direction);

        protected abstract void DestoyBullet();
        protected virtual IEnumerator StartTheCountdownToDestory(float timeToDestroy)
        {
            yield return new WaitForSeconds(timeToDestroy);

            DestoyBullet();
        }
    }
}
