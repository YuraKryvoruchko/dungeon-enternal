using UnityEngine;
using DungeonEternal.Player;

namespace DungeonEternal.Weapons
{
    [RequireComponent(typeof(Firearms))]
    public class WeaponPositioningInstaller : MonoBehaviour, IItem
    {
        [SerializeField] private Vector3 _localPosition = Vector3.zero;
        [SerializeField] private Vector3 _localRotation = Vector3.zero;

        private Weapon _weapon;

        private void Awake()
        {
            _weapon = GetComponent<Weapon>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HandsPlayer player))
            {
                player.PickUpAWeapon(_weapon);

                SetTransFormWeapon(player.HandsTransform);
            }
        }
        private void SetTransFormWeapon(Transform transform)
        {
            if (transform != null)
            {
                gameObject.transform.SetParent(transform);

                ResetPosition();

                gameObject.SetActive(false);

                Destroy(this);
            }
            else
            {
                Debug.LogError("Hands position is null!");
            }
        }
        private void ResetPosition()
        {
            gameObject.transform.localPosition = _localPosition;
            gameObject.transform.localRotation = Quaternion.Euler(_localRotation);
        }

        public void GetItem(HandsPlayer player)
        {
            if(player.HandsTransform != null)
            {
                player.PickUpAWeapon(_weapon);

                SetTransFormWeapon(player.HandsTransform);
            }
            else
            {
                Debug.LogError("HandsTransform is null!");
            }
        }
    }
}
