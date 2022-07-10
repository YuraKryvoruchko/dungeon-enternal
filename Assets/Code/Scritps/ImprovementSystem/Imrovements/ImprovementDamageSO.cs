using System;
using UnityEngine;
using DungeonEternal.Weapons;

namespace DungeonEternal.ImprovementSystem
{
    [CreateAssetMenu(fileName = "ImprovementDamageSO",
                     menuName = "ScriptableObjects/Imrovemetns/ImprovementDamageSO",
                     order = 1)]
    public class ImprovementDamageSO : ImprovementSO
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
                        if (improvementObject.TryGetComponent(out IImprovementDamage improvementWeapon))
                        {
                            switch (_wayToImprove.FunctionType)
                            {
                                case TypeWayToImprove.SetNew:
                                    improvementWeapon.SetNewDamage(Mathf.RoundToInt(_wayToImprove.Value));
                                    improvemetnRun = true;
                                    break;
                                case TypeWayToImprove.IncreaseCapacityBy:
                                    improvementWeapon.IncreaseDamageBy(Mathf.RoundToInt(_wayToImprove.Value));
                                    improvemetnRun = true;
                                    break;
                                case TypeWayToImprove.IncreaseCapacityByInPercentage:
                                    improvementWeapon.IncreaseDamageByInPercentage(_wayToImprove.Value);
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
                        if (improvementObject.TryGetComponent(out IImprovementDamage improvementWeapon))
                        {
                            switch (_wayToImprove.FunctionType)
                            {
                                case TypeWayToImprove.SetNew:
                                    improvementWeapon.SetNewDamage(-Mathf.RoundToInt(_wayToImprove.Value));
                                    break;
                                case TypeWayToImprove.IncreaseCapacityBy:
                                    improvementWeapon.IncreaseDamageBy(-Mathf.RoundToInt(_wayToImprove.Value));
                                    break;
                                case TypeWayToImprove.IncreaseCapacityByInPercentage:
                                    improvementWeapon.IncreaseDamageByInPercentage(-_wayToImprove.Value);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}