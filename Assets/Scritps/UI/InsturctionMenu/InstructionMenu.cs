using System;
using UnityEngine;

namespace DungeonEternal.UI
{
    public class InstructionMenu : MonoBehaviour
    {
        public static event Action OnShowInstructionMenu;
        public static event Action OnCloseInstructionMenu;

        private void OnEnable()
        {
            PauseMenu.OnEnableMenu += DisablePanel;
        }
        private void OnDisable()
        {
            PauseMenu.OnEnableMenu -= DisablePanel;
        }

        private void Start()
        {
            Show();
        }

        public void Show()
        {
            Cursor.lockState = CursorLockMode.Confined;

            Cursor.visible = true;

            OnShowInstructionMenu?.Invoke();
        }
        public void Close()
        {
            Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = false;

            DisablePanel();
        }
        private void DisablePanel()
        {
            OnCloseInstructionMenu?.Invoke();

            gameObject.SetActive(false);
        }
    }
}
