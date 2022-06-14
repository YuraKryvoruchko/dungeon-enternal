using UnityEngine;

namespace DungeonEternal.Weapons
{
    public class WeaponSpeaker : MonoBehaviour
    {
        [SerializeField] private AudioClip _attackClip;
        [SerializeField] private AudioClip _reloadClip;
        [SerializeField] private AudioClip _takingClip;

        private AudioSource _audioSource;

        public void Play(WeaponSpeakerSong weaponSpeakerSong)
        {
            switch (weaponSpeakerSong)
            {
                case WeaponSpeakerSong.Attack:
                    CheckAndPlayClip(_attackClip);
                    break;
                case WeaponSpeakerSong.Reload:
                    CheckAndPlayClip(_reloadClip);
                    break;
                case WeaponSpeakerSong.Taking:
                    CheckAndPlayClip(_takingClip);
                    break;
            }
        }

        private void CheckAndPlayClip(AudioClip audioClip)
        {
            if (audioClip != null)
                _audioSource.PlayOneShot(audioClip);
            else
                Debug.LogWarning(audioClip + ": is null!");
        }
    }
    public enum WeaponSpeakerSong
    {
        Attack,
        Reload,
        Taking
    }
}
