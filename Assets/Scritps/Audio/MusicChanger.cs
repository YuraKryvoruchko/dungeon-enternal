using UnityEngine;
using DungeonEternal.Player;

namespace DungeonEternal.Audio
{
    public class MusicChanger : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        private static BackgroundMusic s_backgroundMusic;

        private void Start()
        {
            if (s_backgroundMusic == null)
                s_backgroundMusic = FindObjectOfType<BackgroundMusic>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out HandsPlayer handsPlayer))
                StartCoroutine(s_backgroundMusic.SetMusic(_clip));
        }
    }
}
