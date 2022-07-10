using System;
using UnityEngine;

namespace DungeonEternal.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _content;

        [SerializeField] private KeyCode _enableAndDisableKeyCode = KeyCode.Escape;

        private bool _onPause = false;

        public static event Action OnEnableMenu;
        public static event Action OnDisableMenu;

        private void Update()
        {
            if (Input.GetKeyDown(_enableAndDisableKeyCode))
            {
                if (_onPause == false)
                    EnableMenu();
                else
                    DisableMenu();
            }
        }

        public void EnableMenu()
        {
            _content.SetActive(true);

            _onPause = true;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            OnEnableMenu?.Invoke();

            Time.timeScale = 0;
        }
        public void DisableMenu()
        {
            OnDisableMenu?.Invoke();

            _content.SetActive(false);

            _onPause = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Time.timeScale = 1;
        }
    }
}
