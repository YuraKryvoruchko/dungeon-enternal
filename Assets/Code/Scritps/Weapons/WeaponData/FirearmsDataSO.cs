using UnityEngine;

namespace DungeonEternal.Weapons.WeaponData
{
    [CreateAssetMenu(fileName = "FirearmsDataSO", 
                     menuName = "ScriptableObjects/WeaponData/FirearmsDataSO",
                     order = 1)]
    public class FirearmsDataSO : ScriptableObject
    {
        [field: SerializeField] public int DataMaxCountStorBullets { get; set; }

        [field: SerializeField] public float DataReloadTime { get; set; }
        [field: SerializeField] public float DataTimeBetweenShots { get; set; }
    }
}
