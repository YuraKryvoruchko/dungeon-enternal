using System;
using UnityEngine;
using DungeonEternal.Weapons;

namespace DungeonEternal.ImprovementSystem
{
    [CreateAssetMenu(fileName = "ImprovementRateOfFireSO",
                     menuName = "ScriptableObjects/Imrovemetns/ImprovementRateOfFireSO",
                     order = 1)]
    public class ImprovementRateOfFireOS : ImprovementSO
    {
        [SerializeField] private WeaponType[] _typesOfWeapons;
        [Space]
        [SerializeField] private WayToImprove _wayToImprove;

        private enum TypeWayToImprove
        {
            SetNew,
            IncreaseCapacityBy,
            IncreaseCapacityByInPercentage
        }

        [Serializable]
        private struct WayToImprove
        {
            [field: SerializeField] public TypeWayToImprove FunctionType { get; set; }
            [field: SerializeField] public float Value { get; set; }
        }

        public override bool RunImprovement(GameObject improvementObject)
        {
            bool improvemetnRun = false;

            if (improvementObject.TryGetComponent(out Weapon weapon))
            {
                for (int i = 0; i < _typesOfWeapons.Length; i++)
                {
                    if (_typesOfWeapons[i] == weapon.WeaponType)
                    {
                        if (improvementObject.TryGetComponent(out IImprovementRateOfFire improvementWeapon))
                        {
                            switch (_wayToImprove.FunctionType)
                            {
                                case TypeWayToImprove.SetNew:
                                    improvementWeapon.SetNewRate(Mathf.RoundToInt(_wayToImprove.Value));
                                    improvemetnRun = true;
                                    break;
                                case TypeWayToImprove.IncreaseCapacityBy:
                                    improvementWeapon.IncreaseRateBy(Mathf.RoundToInt(_wayToImprove.Value));
                                    improvemetnRun = true;
                                    break;
                                case TypeWayToImprove.IncreaseCapacityByInPercentage:
                                    improvementWeapon.IncreaseRateByInPercentage(_wayToImprove.Value);
                                    improvemetnRun = true;
                                    break;
                            }
                        }
                    }
                }
            }

            return improvemetnRun;
        }
        public override void StopImprovement(GameObject improvementObject)
        {
            if (improvementObject.TryGetComponent(out Weapon weapon))
            {
                for (int i = 0; i < _typesOfWeapons.Length; i++)
                {
                    if (_typesOfWeapons[i] == weapon.WeaponType)
                    {
                        if (improvementObject.TryGetComponent(out IImprovementRateOfFire improvementWeapon))
                        {
                            switch (_wayToImprove.FunctionType)
                            {
                                case TypeWayToImprove.SetNew:
                                    improvementWeapon.SetNewRate(-Mathf.RoundToInt(_wayToImprove.Value));
                                    break;
                                case TypeWayToImprove.IncreaseCapacityBy:
                                    improvementWeapon.IncreaseRateBy(-Mathf.RoundToInt(_wayToImprove.Value));
                                    break;
                                case TypeWayToImprove.IncreaseCapacityByInPercentage:
                                    improvementWeapon.IncreaseRateByInPercentage(-_wayToImprove.Value);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
