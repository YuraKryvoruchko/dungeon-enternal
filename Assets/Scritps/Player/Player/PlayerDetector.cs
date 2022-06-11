using UnityEngine;
using DungeonEternal.TrayderImprovement;

namespace DungeonEternal.Player
{ 
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private float _distanse = 1f;

        [SerializeField] private Camera _playerCamera;

        private Purse _purse;

        private void Awake()
        {
            _purse = GetComponent<Purse>();
        }

        private void Update()
        {
            ShootRay();
        }

        private void ShootRay()
        {
            Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);

            Debug.DrawRay(ray.origin, ray.direction * _distanse, Color.green);

            if (Physics.Raycast(ray, out RaycastHit hit, _distanse, 1, QueryTriggerInteraction.UseGlobal))
            {
                if (hit.transform.TryGetComponent(out Trader trader) && Input.GetKeyDown(KeyCode.F))
                    trader.StartTrade(_purse);
            }
        }
    }
}
