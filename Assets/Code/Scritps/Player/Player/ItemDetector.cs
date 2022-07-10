using UnityEngine;

namespace DungeonEternal.Player
{
    [RequireComponent(typeof(HandsPlayer))]
    public class ItemDetector : MonoBehaviour
    {
        private HandsPlayer _palyer;

        private void Start()
        {
            _palyer = GetComponent<HandsPlayer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IItem item))
                item.GetItem(_palyer);
        }
    }
}
