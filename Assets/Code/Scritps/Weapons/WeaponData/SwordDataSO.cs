using UnityEngine;

namespace DungeonEternal.Weapons.WeaponData
{
    [CreateAssetMenu(fileName = "SwordDataSO",
                     menuName = "ScriptableObjects/WeaponData/SwordDataSO",
                     order = 1)]
    public class SwordDataSO : ScriptableObject
    {
        [field: SerializeField] public float Damage { get; set; }
        [field: SerializeField] public float SpeedAttack { get; set; }
    }
}
