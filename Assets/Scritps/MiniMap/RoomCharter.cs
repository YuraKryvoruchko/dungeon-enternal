using System;
using UnityEngine;
using UnityEngine.UI;
using DungeonEternal.Player;

namespace DungeonEternal.MiniMap
{
    public class RoomCharter : MonoBehaviour
    {
        [SerializeField] private Sprite _playerInRoom;
        [SerializeField] private Sprite _playerOutRoom;

        [SerializeField] private Image _image;

        private RoomStatus _roomStatus = RoomStatus.Unvisited;

        public static event Action<RoomCharter> PlayerEnterRoom;
        public static event Action<RoomCharter> PlyaerOutRoom;

        private enum RoomStatus
        {
            Visited,
            Unvisited
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HealthPlayer player))
            {
                if(_roomStatus == RoomStatus.Unvisited)
                    CreateRoomMethod();

                if (_playerInRoom != null)
                    _image.sprite = _playerInRoom;

                PlayerEnterRoom?.Invoke(this);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out HealthPlayer player))
            {
                if (_playerOutRoom != null)
                    _image.sprite = _playerOutRoom;

                PlyaerOutRoom?.Invoke(this);
            }
        }

        private void CreateRoomMethod()
        {
            _roomStatus = RoomStatus.Visited;

            _image.gameObject.SetActive(true);
        }
    }
}
