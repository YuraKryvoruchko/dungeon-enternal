using UnityEngine;

namespace DungeonEternal.Weapons
{
    [RequireComponent(typeof(BulletObjectPool))]
    public class BulletEjector : MonoBehaviour
    {
        private BulletObjectPool _pool;

        private void Start()
        {
            _pool = GetComponent<BulletObjectPool>();

            if (_pool == null)
                Debug.LogError("BulletObjectPool is null!");
        }

        public Bullet EnjectFromPool(Bullet bullet, Vector3 position, Vector3 direction)
        {
            var presenter = _pool.GetPool(bullet);
            presenter.OnCreate(position, direction);

            return presenter;
        }
    }
}
