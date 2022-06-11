using System;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonEternal
{
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] private List<ObjectInfo> _objectInfo;

        private Dictionary<BulletType, Pool> _pools;

        private static ObjectPooler s_instance;

        public static ObjectPooler Instance { get => s_instance; }

        [Serializable]
        public struct ObjectInfo
        {
            [SerializeField] private int _startCount;

            [SerializeField] private GameObject _prefab;

            [SerializeField] private BulletType _type;

            public int StartCount { get => _startCount; }
            public GameObject Prefab { get => _prefab; }
            public BulletType Type { get => _type; }

            public enum ObjectType
            {
                Bullet1,
                Bullet2,
                Bullet3
            }
        }

        private void Awake()
        {
            if (s_instance == null)
                s_instance = this;

            InitPool();
        }

        public GameObject GetObject(BulletType type)
        {
            GameObject instantiateObject = _pools[type].Objects.Count > 0 ?
                _pools[type].Objects.Dequeue() : InstantiateObject(type, _pools[type].Container);

            instantiateObject.SetActive(true); 
            
            return instantiateObject;
        }
        public void DestroyObject(GameObject instantiateObject)
        {
            _pools[instantiateObject.GetComponent<IPooledObejct>().Type].Objects.Enqueue(instantiateObject);

            instantiateObject.SetActive(true);
        }

        private void InitPool()
        {
            _pools = new Dictionary<BulletType, Pool>();

            GameObject empty = new GameObject();

            foreach (ObjectInfo objectInfo in _objectInfo)
            {
                GameObject container = Instantiate(empty, transform, false);

                container.name = objectInfo.Type.ToString();

                _pools[objectInfo.Type] = new Pool(container.transform);

                for (int i = 0; i < objectInfo.StartCount; i++)
                {
                    GameObject go = InstantiateObject(objectInfo.Type, container.transform);

                    _pools[objectInfo.Type].Objects.Enqueue(go);
                }
            }

            Destroy(empty);
        }
        private GameObject InstantiateObject(BulletType type, Transform parent)
        {
            GameObject go = Instantiate(_objectInfo.Find(x => x.Type == type).Prefab, parent);

            go.SetActive(false);

            return go;
        }
    }
}
