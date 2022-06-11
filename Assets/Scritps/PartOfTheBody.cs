using System;
using UnityEngine;

namespace DungeonEternal.Health
{
    [RequireComponent(typeof(BoxCollider))]
    public class PartOfTheBody : MonoBehaviour, IHealth
    {
        public event Action<float> OnTakeDamage;

        public void TakeDamage(float damage)
        {
            OnTakeDamage?.Invoke(damage);
        }
    }
}
