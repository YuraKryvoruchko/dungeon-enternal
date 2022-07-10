using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] Queue<GameObject> _objects;

    private Transform _container;

    public Transform Container { get => _container; private set => _container = value; }
    public Queue<GameObject> Objects { get => _objects; private set => _objects = value; }

    public Pool(Transform container)
    {
        Container = container;

        Objects = new Queue<GameObject>();
    }
}
