using UnityEngine;
using DungeonEternal.Inventories;

public class PackOfCartridges : MonoBehaviour
{
    [SerializeField] private int _bulletCount;

    [SerializeField] private BulletType _cartridgesForTypeWeapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Inventory>())
        {
            AddBullets(other.GetComponent<Inventory>());

            Destroy(gameObject);
        }
    }

    private void AddBullets(Inventory inventory)
    {
        inventory.AddBullets(_cartridgesForTypeWeapon, _bulletCount);
    }
}
