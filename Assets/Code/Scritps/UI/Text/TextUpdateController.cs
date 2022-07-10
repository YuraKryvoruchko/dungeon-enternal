using UnityEngine;
using UnityEngine.UI;
using DungeonEternal.Player;

public class TextUpdateController : MonoBehaviour
{
    [SerializeField] private Text _bulletInWeapon;
    [SerializeField] private Text _bulletInInventory;

    private void Start()
    {
        HandsPlayer.UpdateBulletText += UpdateText;
    }

    private void OnDisable()
    {
        HandsPlayer.UpdateBulletText -= UpdateText;
    }

    private void UpdateText(int bulletInWeapon, int bulletInInventory)
    {
        _bulletInWeapon.text = bulletInWeapon.ToString();
        _bulletInInventory.text = bulletInInventory.ToString();
    }
}
