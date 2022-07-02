using System.Collections.Generic;
using UnityEngine;

namespace DungeonEternal.TrayderImprovement
{
    public class InventoryToImprovements : MonoBehaviour
    {
        [SerializeField] private ImprovementType _improvementType;
        [Space]
        [SerializeField] private List<ImprovementSO> _workedImprovements;
        [SerializeField] private List<ImprovementSO> _stoppedImprovements;

        public void OnEnable()
        {
            Broadcaster.OnBroadcastImprovement += AddImprovement;

            foreach(var improvement in _workedImprovements)
                improvement.RunImprovement(gameObject);
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
            foreach (var stoppedImprovement in _stoppedImprovements)
            {
                if (stoppedImprovement == improvement)
                {
                    _workedImprovements.Add(stoppedImprovement);
                    _stoppedImprovements.Remove(stoppedImprovement);

                    stoppedImprovement.RunImprovement(gameObject);
                }
            }
        }
        private void AddImprovements(ImprovementSO[] improvements)
        {
            for (int i = 0; i < improvements.Length; i++)
                AddImprovement(improvements[i]);
        }
        private void DeleteImrovement(ImprovementSO improvement)
        {
            if (_workedImprovements.Contains(improvement) == true)
            {
                improvement.StopImprovement(gameObject);

                _workedImprovements.Remove(improvement);
            }
        }
        private void DeleteImrovements(ImprovementSO[] improvements)
        {
            for (int i = 0; i < improvements.Length; i++)
                DeleteImrovement(improvements[i]);
        }
        private void DeleteAllImrovements()
        {
            _workedImprovements.Clear();
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
