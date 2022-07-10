using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace DungeonEternal.Weapons
{
    public class M249 : Firearms
    {
        [SerializeField] private float _xSpreadBot;
        [SerializeField] private float _ySpreadBot;

        public override event Action OnAttack;
        public override event Action WeaponEmpty;

        public override void Attack()
        {
            throw new NotImplementedException();
        }
        public override void Attack(Transform target)
        {
            if (WeaponForState == WeaponState.Free && CanShoot == true)
            {
                Shoot(target);

                StartCoroutine(ToDelayOnFiringootTymer());
            }
        }
        private void Shoot(Transform target)
        {
            if (StorÑapacity > 0)
            {
                WeaponForState = WeaponState.OnShoot;

                AudioSource.PlayOneShot(AudioShoot);

                Animator.SetTrigger(NameShootParameters);

                for (int i = 0; i < BulletsNumberInOneShoot; i++)
                {
                    Vector3 direction = CalculateAndGetDirection(target);

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
        private Vector3 CalculateAndGetDirection(Transform target)
        {
            Vector3 direction = target.position - BulletPoint.position;

            Vector3 startTargetPosition = target.position;
            Quaternion startTargetRotation = target.rotation;

            target.rotation = Quaternion.LookRotation(direction, Vector3.forward);

            Vector3 targetPoint = new Vector3(Random.Range(target.position.x + _xSpreadBot, target.position.x - _xSpreadBot),
                                              Random.Range(target.position.y + _ySpreadBot, target.position.y - _ySpreadBot),
                                              target.position.z);

            target.position = startTargetPosition;
            target.rotation = startTargetRotation;

            direction = targetPoint - BulletPoint.position;

            return direction.normalized;
        }
    }
}
