using System;
using UnityEngine;

namespace DungeonEternal.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private string _modelWeapon;

        public abstract event Action OnAttack;
        public string ModelWeapon { get => _modelWeapon; }

        public abstract void Attack();
        public abstract void Attack(Transform target);
    }
}
