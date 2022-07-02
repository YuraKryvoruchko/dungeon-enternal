using System;
using System.Collections.Generic;
using UnityEngine;
using DungeonEternal.TrayderImprovement;

namespace DungeonEternal.Weapons
{
    public interface IWeaponDataOnCreate
    {
        public int DataMaxCountStorBullets { get; set; }

        public float DataReloadTime { get; set; }
        public float DataTimeBetweenShots { get; set; }
    }

    public class ShootGun : Firearms, IImprovementStoreCapacity, IImprovementReloadSpeed
    {
        [Space]
        [SerializeField] private ImprovementCharacteristics _improvementCharacteristics;

        private static Dictionary<string, ImprovementCharacteristics> s_keyValuePairs;

        public override event Action OnAttack;
        public override event Action WeaponEmpty;

        private void Start()
        {
            //if (s_keyValuePairs.ContainsKey(ModelWeapon) == false)
            //{
            //    s_keyValuePairs.Add(ModelWeapon, _improvementCharacteristics);
            //}
            //else
            //{
            //    if (s_keyValuePairs.TryGetValue(ModelWeapon, out ImprovementCharacteristics improvementCharacteristics))
            //        _improvementCharacteristics = improvementCharacteristics;
            //}

            MaxCountStorBullets = _improvementCharacteristics.DataMaxCountStorBullets;
        }

        [Serializable]
        public struct ImprovementCharacteristics
        {
            [field: SerializeField] public int DataMaxCountStorBullets { get; set; }

            [field: SerializeField] public float DataReloadTime { get; set; }
            [field: SerializeField] public float DataTimeBetweenShots { get; set ; }
        }

        public override void Attack()
        {
            if (WeaponForState == WeaponState.Free && CanShoot == true)
            {
                StartCoroutine(ToDelayOnFiringootTymer());

                Shoot();
            }
        }
        
        private void Shoot()
        {
            if (StorСapacity > 0)
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

                StorСapacity -= IntbulletsEjectionInOneShot;

                Debug.Log("Fire " + UsedTypeOfBullets);

                WeaponForState = WeaponState.Free;

                OnAttack?.Invoke();
            }
            else
            {
                WeaponEmpty?.Invoke();
            }
        }

        public void SetNewCapacity(int newCapacity)
        {
            MaxCountStorBullets = newCapacity;
            _improvementCharacteristics.DataMaxCountStorBullets = newCapacity;
        }
        public void IncreaseCapacityBy(int capacity)
        {
            MaxCountStorBullets += capacity;
            _improvementCharacteristics.DataMaxCountStorBullets += capacity;
        }
        public void IncreaseCapacityByInPercentage(float percentage)
        {
            int capacity = Mathf.RoundToInt(MaxCountStorBullets * percentage);

            MaxCountStorBullets += capacity;
            _improvementCharacteristics.DataMaxCountStorBullets += capacity;
        }

        public void SetNewReloadSpeed(float newReloadSpeed)
        {
            throw new NotImplementedException();
        }
        public void IncreaseReloadSpeedBy(float reloadSpeed)
        {
            throw new NotImplementedException();
        }
        public void IncreaseReloadSpeedByInPercentage(float percentage)
        {
            throw new NotImplementedException();
        }
    }
}
