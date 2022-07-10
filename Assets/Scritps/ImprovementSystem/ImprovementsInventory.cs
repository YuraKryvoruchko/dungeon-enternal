using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonEternal.ImprovementSystem
{
    [CreateAssetMenu(fileName = "ImprovementsInventoryFor", 
                     menuName = "ScriptableObjects/Imrovemetns/ImprovementsInventory", 
                     order = 1)]
    public class ImprovementsInventory : ScriptableObject
    {
        [SerializeField] private ImprovementType _improvementType;
        [Space]
        [SerializeField] private List<ImprovementSO> _workedImprovements;
        [SerializeField] private List<ImprovementSO> _stoppedImprovements;

        public event Action<ImprovementSO> OnRunImprovement;
        public event Action<ImprovementSO> OnReturnImprovement;

        public void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private bool VerifyImprovementType(ImprovementSO improvement)
        {
            if (improvement.ImprovementType == _improvementType)
                return true;

            Debug.LogWarning("Improvement improvementType != base improvementType");

            return false;
        }

        public void AddImprovement(ImprovementSO improvement)
        {
            if (VerifyImprovementType(improvement) == false)
                return;

            if(_workedImprovements.Contains(improvement) == false)
            {
                _workedImprovements.Add(improvement);

                OnRunImprovement?.Invoke(improvement);
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
                OnReturnImprovement?.Invoke(improvement);

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
}
