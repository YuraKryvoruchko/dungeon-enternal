using UnityEngine;
using DungeonEternal.Player;

namespace DungeonEternal.Weapons
{
    public class WeaponLayerInstaller : MonoBehaviour
    {
        [SerializeField] private int _layerIndex = 11;
        [Space]
        [SerializeField] private GameObject[] _partsWeapon;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HandsPlayer player))
                ChangeLayer();
        }

        private void ChangeLayer()
        {
            for (int i = 0; i < _partsWeapon.Length; i++)
                _partsWeapon[i].layer = _layerIndex;
        }
    }
}
