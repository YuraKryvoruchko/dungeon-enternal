using System.Collections;
using UnityEngine;

namespace DungeonEternal.MiniMap
{
    [RequireComponent(typeof(Camera))]
    public class MiniMap : MonoBehaviour
    {
        [SerializeField] private float _speedChangePosition = 0.5f;

        [SerializeField] private float _heightToCamera = 30f;

        private Camera _miniMapCamera;

        private Transform _playerTransform;

        private void OnEnable()
        {
            RoomCharter.PlayerEnterRoom += SetPosition;
        }
        private void OnDisable()
        {
            RoomCharter.PlayerEnterRoom -= SetPosition;
        }
        private void Awake()
        {
            _miniMapCamera = GetComponent<Camera>();
        }

        private void SetPosition(RoomCharter room)
        {
            Vector3 roomPosition = room.transform.position;

            Vector3 position = new Vector3(roomPosition.x, _heightToCamera, roomPosition.z);

            StartCoroutine(SetPosition(position));
        }
        private IEnumerator SetPosition(Vector3 position)
        {
            while(transform.position != position)
            {
                transform.position = Vector3.Lerp(transform.position, position, _speedChangePosition);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
