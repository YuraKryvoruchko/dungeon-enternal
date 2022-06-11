using UnityEngine;
using UnityEngine.UI;
using DungeonEternal.Player;

namespace DungeonEternal.MiniMap
{
    public class RoomOnMapVisualisator : MonoBehaviour
    {
        [SerializeField] private Image _imageRoomPrefab;

        [SerializeField] private Color _colorInRoom;
        [SerializeField] private Color _colorBehindRoom;

        [SerializeField] private bool _isUseColor;

        [SerializeField] private Sprite _spriteOnRoom;
        [SerializeField] private Sprite _spriteBehindRoom;

        [SerializeField] private Image _imageRoom;

        private bool _isRoomFound = false;

        public Image ImageRoom { get => _imageRoom; set => _imageRoom = value; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<HandsPlayer>())
            {
                MiniMap miniMap = FindObjectOfType<MiniMap>();

                if (!_isRoomFound)
                {
                    Vector3 vector = new Vector3(miniMap.transform.position.x, miniMap.transform.position.y, transform.position.z);

                    _imageRoom = Instantiate(_imageRoomPrefab, vector, Quaternion.identity);

                    _imageRoom.transform.SetParent(miniMap.transform);

                    _isRoomFound = true;
                }

                CheckingAndChangingTheView(inRoom: true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<HandsPlayer>())
            {
                CheckingAndChangingTheView(inRoom: false);
            }
        }

        private void CheckingAndChangingTheView(bool inRoom)
        {
            if (_isUseColor)
            {
                print("1");
                if (inRoom)
                {
                    _imageRoom.color = _colorInRoom;
                }
                else
                {
                    _imageRoom.color = _colorBehindRoom;
                }

            }
            else
            {
                print("2");
                if (inRoom)
                {
                    _imageRoom.sprite = _spriteOnRoom;
                }
                else
                {
                    _imageRoom.sprite = _spriteBehindRoom;
                }
            }
        }
    }
}
