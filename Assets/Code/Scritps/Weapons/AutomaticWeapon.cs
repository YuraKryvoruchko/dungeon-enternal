using System;
using System.Collections;
using UnityEngine;

namespace DungeonEternal.Weapons
{
    public class AutomaticWeapon : Firearms
    {
        [Header("Automatic properties")]
        [SerializeField] private float _fireRate;

        public override event Action OnAttack;
        public override event Action WeaponEmpty;

        public override void Attack()
        {
            if(CanShoot == true)
            {
                StartCoroutine(AutomaticShoot());
                StartCoroutine(ToDelayOnFiringootTymer());
            }
        }
        private IEnumerator AutomaticShoot()
        {
            if (WeaponForState == WeaponState.Free)
            {
                while (Input.GetMouseButton(0))
                {
                    SingleShooting();

                    yield return new WaitForSeconds(_fireRate);
                }
            }
        }
        private void SingleShooting()
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
