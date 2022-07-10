using System;
using UnityEngine;

namespace DungeonEternal.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private string _modelWeapon;
        [Space]
        [SerializeField] private WeaponType _weaponType;

        public abstract event Action OnAttack;

        public string ModelWeapon { get => _modelWeapon; }
         
        public WeaponType WeaponType { get => _weaponType; }

        public abstract void Attack();
        public abstract void Attack(Transform target);
    }

    public enum WeaponType
    {
        Pistol,
        SubmachineGun,
        Submachine,
        SemiAutomaticRifle,
        Rifle,
        MachineGun,
        Shotgun,
        Grenade,
        GrenadeLauncher,
        Sword,
        Bow
    }
}
