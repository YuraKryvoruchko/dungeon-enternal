using System.Collections.Generic;
using UnityEngine;

namespace DungeonEternal.Weapons
{
    public class BulletObjectPool : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPresenterTemplate;

        private List<Bullet> _available = new List<Bullet>();
        private List<Bullet> _inUse = new List<Bullet>();

        public Bullet GetPool(Bullet bullet)
        {
            if (_bulletPresenterTemplate == null)
            {
                Debug.LogError("Prefab is null!");
                return default;
            }

            Bullet presenter = null;

            if (_available.Count == 0)
            {
                presenter = Instantiate(_bulletPresenterTemplate);
                presenter.OnHitOrDestroy += () => Release(presenter);
            }
            else
            {
                presenter = _available[0];
                presenter.gameObject.SetActive(true);
                _available.Remove(presenter);
            }

            _inUse.Add(presenter);
            return presenter;
        }
        public void Release(Bullet presenter)
        {
            if (_inUse.Remove(presenter) == false)
                return;

            presenter.gameObject.SetActive(false);
            _available.Add(presenter);
        }
    }
}   
