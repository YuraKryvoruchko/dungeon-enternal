using UnityEngine;

namespace DungeonEternal.UI
{
    public class ControlSettings : MonoBehaviour
    {
        private void OnEnable()
        {
            PauseMenu.OnDisableMenu += ResumeGame;
        }
        private void OnDisable()
        {
            PauseMenu.OnDisableMenu -= ResumeGame;
        }

        public void ResumeGame()
        {
            gameObject.SetActive(false);
        }
    }
}
