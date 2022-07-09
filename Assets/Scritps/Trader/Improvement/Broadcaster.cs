using System;
using UnityEngine;
using DungeonEternal.ImprovementSystem;

namespace DungeonEternal.TrayderImprovement
{
    [RequireComponent(typeof(TraderCell))]
    public class Broadcaster : MonoBehaviour
    {
        [SerializeField] private ImprovementSO _improvementSO;

        private TraderCell _traderCell;

        public static event Action<ImprovementSO> OnBroadcastImprovement;
        
        private void Awake()
        {
            _traderCell = GetComponent<TraderCell>();
        }
        private void OnEnable()
        {
            _traderCell.OnBuyCell += BroadcastImprovement;
        }
        private void OnDisable()
        {
            _traderCell.OnBuyCell -= BroadcastImprovement;
        }

        public void BroadcastImprovement()
        {
            OnBroadcastImprovement?.Invoke(_improvementSO);
        }
    }
}
