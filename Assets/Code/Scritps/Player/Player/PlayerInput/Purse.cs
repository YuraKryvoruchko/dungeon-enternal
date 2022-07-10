using UnityEngine;

namespace DungeonEternal.Player
{
    public class Purse : MonoBehaviour
    {
        [SerializeField] private int _coins = 50;

        public int Coins { get => _coins; set => _coins = value; }
    }
}
