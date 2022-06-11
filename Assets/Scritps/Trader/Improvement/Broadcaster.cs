using System;
using UnityEngine;

namespace DungeonEternal.TrayderImprovement
{
    [RequireComponent(typeof(TraderCell))]
    public class Broadcaster : MonoBehaviour
    {
        [SerializeField] private ImprovementSO _improvementSO;

        private TraderCell _traderCell;
        
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

        public static event Action<ImprovementSO> OnBroadcastImprovement;

        public void BroadcastImprovement()
        {
            OnBroadcastImprovement?.Invoke(_improvementSO);
        }
    }
}
