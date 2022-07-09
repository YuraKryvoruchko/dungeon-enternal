using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DungeonEternal.TrayderImprovement;
using DungeonEternal.Weapons;

namespace DungeonEternal.ImprovementSystem
{
    [CreateAssetMenu(fileName = "InventoryForImprovemet", 
                     menuName = "ScriptableObjects/Imrovemetns/InventoryForImprovemet", 
                     order = 1)]
    public class WeaponImprovemetnSO : ScriptableObject
    {
        [SerializeField] private ImprovementType _improvementType;
        [SerializeField] private WeaponType _weaponType;
        [Space]
        [SerializeField] private List<ImprovementSO> _workedImprovements;
        [SerializeField] private List<ImprovementSO> _stoppedImprovements;

        public event Action<ImprovementSO> OnRunImprovement;
        public event Action<ImprovementSO> OnReturnImprovement;

        public void Awake()
        {
            Broadcaster.OnBroadcastImprovement += AddImprovement;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Broadcaster.OnBroadcastImprovement -= AddImprovement;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private bool VerifyImprovementType(ImprovementSO improvement)
        {
            if (improvement.ImprovementType != _improvementType)
                return false;

            if (VerifyWeaponType(improvement) == true)
                return true;
            else
                return false;
        }
        private bool VerifyWeaponType(ImprovementSO improvement)
        {
            if (improvement == new ImrovemetnCapacitySO())
                return false;

            return true;
        }

        private void AddImprovement(ImprovementSO improvement)
        {
            if (VerifyImprovementType(improvement) == false)
                return;

            foreach (var stoppedImprovement in _stoppedImprovements)
            {
                if (stoppedImprovement == improvement)
                {
                    _workedImprovements.Add(stoppedImprovement);
                    _stoppedImprovements.Remove(stoppedImprovement);

                    OnRunImprovement?.Invoke(improvement);
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
