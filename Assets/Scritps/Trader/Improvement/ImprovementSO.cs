using UnityEngine;

namespace DungeonEternal.TrayderImprovement
{
    [CreateAssetMenu(fileName = "ImprovementSO", menuName = "ScriptableObjects/Improvement", order = 2)]
    public class ImprovementSO : ScriptableObject
    {
        [SerializeField] private ImprovementType _improvementType;

        public ImprovementType ImprovementType { get => _improvementType; }

        public void RunImprovement()
        {
            
        }
    }
}
