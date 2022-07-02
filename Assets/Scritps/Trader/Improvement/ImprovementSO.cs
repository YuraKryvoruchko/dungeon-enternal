using UnityEngine;

namespace DungeonEternal.TrayderImprovement
{
    public abstract class ImprovementSO : ScriptableObject
    {
        [SerializeField] private ImprovementType _improvementType;
        [Space]
        [SerializeField] private ImprovementStatus _status;

        private GameObject _improvementObject; 
        
        protected enum ImprovementStatus
        {
            Working,
            Stopped
        }

        protected ImprovementStatus Status { get => _status; set => _status = value; }
        protected GameObject ImprovementObject { get => _improvementObject; set => _improvementObject = value; }

        public ImprovementType ImprovementType { get => _improvementType; }

        public abstract bool RunImprovement(GameObject improvementObject);
        public abstract void StopImprovement(GameObject improvementObject);
    }
}
