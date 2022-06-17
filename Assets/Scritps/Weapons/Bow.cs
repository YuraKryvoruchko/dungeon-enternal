using System;
using System.Collections;
using UnityEngine;

namespace DungeonEternal.Weapons
{
    public class Bow : Firearms
    {
        [Header("Bow properties")]
        [SerializeField] private float _timeToMaxEnergy = 3f;
        [SerializeField] private float _maxEnergy = 50f;

        [Header("Bow animation properties")]
        [SerializeField] private string _namePullingParameters = "Pulling";

        private float _energy = 0f;

        private const float MIN_ENERGY = 0f;

        public override event Action OnAttack;
        public override event Action WeaponEmpty;

        public override void Attack()
        {
            if (WeaponForState == WeaponState.Free && CanShoot == true)
            {
                if (Stor—apacity > 0)
                    StartCoroutine(AccumulateEnergy());
                else
                    WeaponEmpty?.Invoke();

                StartCoroutine(ToDelayOnFiringootTymer());
            }
        }
        private IEnumerator AccumulateEnergy()
        {
            var arrow = CreateArrow(Vector3.zero);

            arrow.transform.SetParent(BulletPoint);
            arrow.transform.localRotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);

            arrow.GetComponent<Collider>().enabled = false;

            Animator.SetTrigger(_namePullingParameters);

            float currentTime = 0f;

            while (true)
            {
                if (currentTime <= _timeToMaxEnergy)
                {
                    currentTime += Time.deltaTime;

                    _energy = Mathf.Lerp(MIN_ENERGY, _maxEnergy, currentTime / _timeToMaxEnergy);
                }
                else
                {
                    _energy = _maxEnergy;
                }

                Debug.Log(_energy);

                if (Input.GetMouseButton(0) == false)
                    break;

                yield return new WaitForEndOfFrame();
            }

            Debug.Log("Energy: " + _energy);

            Vector3 direction = CalculateDirection(XSpread, YSpread);

            arrow.transform.SetParent(null);
            arrow.transform.localRotation = Quaternion.LookRotation(direction, Vector3.forward);

            Shoot(arrow, _energy);

            arrow.GetComponent<Collider>().enabled = true;

            WeaponEmpty?.Invoke();

            _energy = 0;

            yield return null;
        }
        private void Shoot(Arrow arrow, float energy)
        {
            Vector3 direction = CalculateDirection(XSpread, YSpread);

            arrow.SetDirection(direction);
            arrow.ToPush(energy);

            Animator.SetTrigger(NameShootParameters);

            Stor—apacity -= IntbulletsEjectionInOneShot;

            Debug.Log("Fire " + UsedTypeOfBullets);

            OnAttack?.Invoke();
        }
        private Arrow CreateArrow(Vector3 direction)
        {
            var bullet = BulletEjector.EnjectFromPool(BulletPrefab, BulletPoint.position, direction);

            if (bullet.TryGetComponent(out Arrow arrow))
                return arrow;
            else
                return default;
        }
    }
}
