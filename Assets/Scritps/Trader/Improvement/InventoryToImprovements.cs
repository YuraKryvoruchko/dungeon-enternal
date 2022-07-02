using System.Collections.Generic;
using UnityEngine;

namespace DungeonEternal.TrayderImprovement
{
    public class InventoryToImprovements : MonoBehaviour
    {
        [SerializeField] private ImprovementType _improvementType;
        [Space]
        [SerializeField] private List<ImprovementSO> _improvements;

        public void OnEnable()
        {
            Broadcaster.OnBroadcastImprovement += AddImprovement;
        }
        private void OnDisable()
        {
            Broadcaster.OnBroadcastImprovement -= AddImprovement;
        }

        private bool VerifyImprovementType(ImprovementSO improvement)
        {
            if (improvement.ImprovementType == _improvementType)
                return true;
            else
                return false;
        }
        private void AddImprovement(ImprovementSO improvement)
        {
            if (VerifyImprovementType(improvement) == false)
                return;

            if (_improvements.Contains(improvement) == false)
            {
                _improvements.Add(improvement);

                bool isActiveImprovement = improvement.RunImprovement(gameObject);

                if (isActiveImprovement == false)
                    DeleteImrovement(improvement);
            }
        }
        private void AddImprovements(ImprovementSO[] improvements)
        {
            for (int i = 0; i < improvements.Length; i++)
                AddImprovement(improvements[i]);
        }
        private void DeleteImrovement(ImprovementSO improvement)
        {
            if (_improvements.Contains(improvement) == true)
            {
                improvement.StopImprovement(gameObject);

                _improvements.Remove(improvement);
            }
        }
        private void DeleteImrovements(ImprovementSO[] improvements)
        {
            for (int i = 0; i < improvements.Length; i++)
                DeleteImrovement(improvements[i]);
        }
        private void DeleteAllImrovements()
        {
            _improvements.Clear();
        }
    }
    public enum ImprovementType
    {
        Player,
        Weapon,
        Bullet,
        Item
    }
}
