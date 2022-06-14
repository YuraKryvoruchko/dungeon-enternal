using System;
using UnityEngine;

namespace DungeonEternal.Weapons
{
    public class SemiAutomaticWeapon : Firearms
    {
        public override event Action OnAttack;
        public override event Action WeaponEmpty;

        public override void Attack()
        {
            if (WeaponForState == WeaponState.Free && CanShoot == true)
            {
                Shoot();

                StartCoroutine(ToDelayOnFiringootTymer());
            }
        }

        private void Shoot()
        {
            if (StorÑapacity > 0)
            {
                WeaponForState = WeaponState.OnShoot;

                AudioSource.PlayOneShot(AudioShoot);

                Animator.SetTrigger(NameShootParameters);

                for (int i = 0; i < BulletsNumberInOneShoot; i++)
                {
                    Vector3 direction = CalculateDirection(XSpread, YSpread);
                    Debug.DrawRay(BulletPoint.position, direction, Color.black, 100);

                    BulletEjector.EnjectFromPool(BulletPrefab, BulletPoint.position, direction);
                }

                StorÑapacity -= IntbulletsEjectionInOneShot;

                Debug.Log("Fire " + UsedTypeOfBullets);

                WeaponForState = WeaponState.Free;

                OnAttack?.Invoke();
            }
            else
            {
                WeaponEmpty?.Invoke();
            }
        }
    }
}
