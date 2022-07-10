    using UnityEngine;
using UnityEngine.UI;
using DungeonEternal.Player;

public class PictureOfNumberOfLives : MonoBehaviour
{
    [SerializeField] private Image _liveSlider;

    [SerializeField] private GameObject _panelGameOver;

    private void OnEnable()
    {
        HealthPlayer.MyHitPointsWasTakenAway += UpdateHealth;
    }
    private void OnDisable()
    {
        HealthPlayer.MyHitPointsWasTakenAway -= UpdateHealth;
    }

    private void UpdateHealth(float countHealth)
    {
        _liveSlider.fillAmount = countHealth;

        HealthCheck(countHealth);
    }

    private void HealthCheck(float countHealth)
    {
        if (countHealth <= 0)
        {
            _panelGameOver.SetActive(true);
        }
    }
}
