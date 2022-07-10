using System;
using UnityEngine;

namespace DungeonEternal.TrayderImprovement
{
    public class TraderCell : MonoBehaviour
    {
        [SerializeField] private int _price;

        public int Price { get => _price; }

        public event Action<TraderCell> OnTryBuyCell;
        public event Action OnBuyCell;

        public void BuyCell()
        {
            OnTryBuyCell?.Invoke(this);
        }
        public void Buy()
        {
            OnBuyCell?.Invoke();

            Destroy(gameObject);
        }
    }
}
