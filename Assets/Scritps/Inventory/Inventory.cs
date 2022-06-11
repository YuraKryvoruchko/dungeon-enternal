using System.Collections.Generic;
using UnityEngine;
using DungeonEternal.Weapons;
using System;

namespace DungeonEternal.Inventories
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _waypoinsInInventory;

        [SerializeField] private List<Bullet> _bulletInInventory;

        private int a = 1;
        private const int BULLET_COUNT_IS_ZERO = 0;

        public List<Weapon> WaypoinsInInventory { get => _waypoinsInInventory; }

        [Serializable]
        private class Bullet
        {
            [SerializeField] private int _count;

            [SerializeField] private BulletType _type;
            public ref int GetBullet()
            {
                return ref _count;
            }
            public BulletType GetTypeBullet()
            {
                return _type;
            }
        }
        
        public int GetBullets(BulletType typeGun, int bulletCount)
        {
            int bulletCountInInventory = StandOutBullets(typeGun, bulletCount);

            return bulletCountInInventory;
        }
        public int GetBulletCount(BulletType typeGun)
        {
            int bulletCount = DeterminingTheTypeOfCartridge(typeGun);

            return bulletCount;
        }
        public void AddBullets(BulletType typeGun, int bulletCount)
        {
            if (bulletCount >= 0)
                DeterminingTheTypeOfCartridge(typeGun) += bulletCount;
            else
                Debug.Log("You can`t add bullets count with negative numbers");
        }
        public void AddWeaponToInventory(Weapon addingWeapon)
        {
            if (CheckForACopy(addingWeapon, _waypoinsInInventory) == false)
                WaypoinsInInventory.Add(addingWeapon);
        }

        private ref int DeterminingTheTypeOfCartridge(BulletType typeGun)
        {
            ref int enux = ref a;

            foreach (Bullet bullet in _bulletInInventory)
            {
                if (typeGun == bullet.GetTypeBullet())
                {
                    ref int bulletCount = ref bullet.GetBullet();

                    return ref bulletCount;
                }
            }

            return ref enux;
        }
        private int StandOutBullets(BulletType typeGun, int bulletCountInWeapon)
        {
            int bulletCount = DeterminingTheTypeOfCartridge(typeGun);

            if (bulletCount >= bulletCountInWeapon)
            {
                bulletCount -= bulletCountInWeapon;

                DeterminingTheTypeOfCartridge(typeGun) = bulletCount;

                return bulletCountInWeapon;
            }
            else
            {
                if (DeterminingTheTypeOfCartridge(typeGun) > 0)
                {
                    DeterminingTheTypeOfCartridge(typeGun) -= bulletCount;

                    return bulletCount;
                }
                else
                {
                    Debug.Log("Inventory is empty");

                    return BULLET_COUNT_IS_ZERO;
                }
            }
        }
        private bool CheckForACopy(Weapon addingWeapon, List<Weapon> waypoinsInInventory)
        {
            bool isCopy = false;

            foreach (Weapon gunFromInventory in waypoinsInInventory)
            {
                if(addingWeapon.ModelWeapon == gunFromInventory.ModelWeapon)
                {
                    isCopy = true;

                    if(addingWeapon.TryGetComponent(out IFirearms weapon))
                        DeterminingTheTypeOfCartridge(weapon.UsedTypeOfBullets) += weapon.BulletsConversionRate;

                    Destroy(addingWeapon.gameObject);   
                }
            }

            return isCopy;
        }
    }
}
