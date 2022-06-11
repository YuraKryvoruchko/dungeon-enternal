using UnityEngine;
using DungeonEternal.Weapons;
using DungeonEternal.Inventories;

namespace DungeonEternal.Player
{
    [RequireComponent(typeof(Inventory))]
    public class WeaponDetector : MonoBehaviour
    {
        private Inventory _inventory;

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out Weapon weapon))
                _inventory.AddWeaponToInventory(weapon);
        }
    }
}
