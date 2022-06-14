using System;
using UnityEngine;
using DungeonEternal.Weapons;

namespace DungeonEternal.Health
{
    [RequireComponent(typeof(BoxCollider))]
    public class PartOfTheBody : MonoBehaviour, IHealth, IEjection
    {
        public event Action<float> OnTakeDamage;
        public event Action<Vector3, float> OnEjection;


        public void TakeDamage(float damage)
        {
            OnTakeDamage?.Invoke(damage);
        }
        public void Eject(Vector3 direction, float energy)
        {
            OnEjection?.Invoke(direction, energy);
        }
    }
}
