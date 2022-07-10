using UnityEngine;
using UnityEngine.Events;

namespace DungeonEternal.TrayderImprovement
{
    [RequireComponent(typeof(TraderCell))]
    public class Broadcaster : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnBroadcastImprovement;    

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

        public void BroadcastImprovement()
        {
            OnBroadcastImprovement?.Invoke();
        }
    }
}
