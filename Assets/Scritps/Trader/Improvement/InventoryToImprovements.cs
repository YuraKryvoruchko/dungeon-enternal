using System.Collections.Generic;
using UnityEngine;

namespace DungeonEternal.TrayderImprovement
{
    public interface IWeaponImprovement<T>
    {
    }
    public class InventoryToImprovements : MonoBehaviour
    {
        [SerializeField] private ImprovementType _improvementType;
        [Space]
        [SerializeField] private List<ImprovementSO> _improvements;

        private void OnEnable()
        {
            Broadcaster.OnBroadcastImprovement += VerifyImprovementType;
        }
        private void OnDisable()
        {
            Broadcaster.OnBroadcastImprovement -= VerifyImprovementType;
        }

        private void VerifyImprovementType(ImprovementSO improvement)
        {
            if (improvement.ImprovementType == _improvementType)
                AddImprovement(improvement);
        }
        private void AddImprovement(ImprovementSO improvement)
        {
            if (_improvements.Contains(improvement) == false)
            {
                _improvements.Add(improvement);

                improvement.RunImprovement();
            }
        }
        private void DeleteImrovement(ImprovementSO improvement)
        {
            if (_improvements.Contains(improvement) == true)
                _improvements.Remove(improvement);
        }
    }
    public enum ImprovementType
    {
        Player,
        Weapon,
        Enemy,
        Item
    }
}
