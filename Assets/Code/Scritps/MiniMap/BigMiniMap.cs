using System;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonEternal.MiniMap
{
    public class BigMiniMap : MonoBehaviour
    {
        [SerializeField] private Camera _miniMapCamera;

        [Space]
        [SerializeField] private float _speedChengCameraSize = 2.5f;
        [SerializeField] private float _speedChengTransform = 1f;

        [Space]
        [SerializeField] private float _maxHeightToCamera = 50f;
        [SerializeField] private float _minHeightToCamera = 10f;

        [Space]
        [SerializeField] private RawImage _bigMapImage;

        private float _mouseScrollWheel = 0;

        private float _startCameraSize;
        private Vector3 _startCameraPosition;

        private Vector3 _oldCameraPosition;

        private bool _mapEnable = false;

        public static event Action OnEnableBigMap;
        public static event Action OnDisableBigMap;

        private void OnEnable()
        {
            RoomCharter.PlayerEnterRoom += SavePostion;
        }
        private void OnDisable()
        {
            RoomCharter.PlayerEnterRoom -= SavePostion;
        }
        private void Start()
        {
            _startCameraSize = _miniMapCamera.orthographicSize;
            _startCameraPosition = _miniMapCamera.transform.position;

            _mapEnable = _bigMapImage.gameObject.activeSelf;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U) && _bigMapImage.gameObject.activeSelf == false)
                EnableMap();
            else if (Input.GetKeyDown(KeyCode.U) && _bigMapImage.gameObject.activeSelf == true)
                DisableMap();

            if(_mapEnable == true)
            {
                CameraDrag();

                ChangeCameraSize();
            }
        }

        private void EnableMap()
        {
            _bigMapImage.gameObject.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            _mapEnable = true;

            _startCameraPosition = _miniMapCamera.transform.position;

            OnEnableBigMap?.Invoke();
        }
        private void DisableMap()
        {
            _bigMapImage.gameObject.SetActive(false);

            _miniMapCamera.orthographicSize = _startCameraSize;
            _miniMapCamera.transform.position = _startCameraPosition;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            _mapEnable = false;

            OnDisableBigMap?.Invoke();

        }
        private void CameraDrag() 
        {
            if (Input.GetMouseButtonDown(0))
            {
                _oldCameraPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 deltaPosition = _oldCameraPosition - Input.mousePosition;

                _miniMapCamera.transform.position += new Vector3(deltaPosition.x * _speedChengTransform, 0, 
                    deltaPosition.y * _speedChengTransform);

                _oldCameraPosition = Input.mousePosition;
            }
        }
        private void ChangeCameraSize()
        {
            _mouseScrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");

            if (_mouseScrollWheel != 0)
            {
                if (_mouseScrollWheel > 0 && _minHeightToCamera < _miniMapCamera.orthographicSize)
                    _miniMapCamera.orthographicSize -= _speedChengCameraSize;
                else if (_mouseScrollWheel < 0 && _maxHeightToCamera > _miniMapCamera.orthographicSize)
                    _miniMapCamera.orthographicSize += _speedChengCameraSize;
            }
        }
        private void SavePostion(RoomCharter room)
        {
            Vector3 roomPosition = room.transform.position;

            _startCameraPosition = new Vector3(roomPosition.x, transform.position.y, roomPosition.z);
        }
    }
}
