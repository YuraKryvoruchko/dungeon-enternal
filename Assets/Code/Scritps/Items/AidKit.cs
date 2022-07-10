using UnityEngine;
using DungeonEternal.Player;

public class AidKit : MonoBehaviour
{
    [SerializeField] private float _amountOfReplenishedHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthPlayer>())
        {
            HealthPlayer player = other.GetComponent<HealthPlayer>();

            if (player.Health < player.MaksHealth)
            {
                player.TakeDamage(-_amountOfReplenishedHealth);

                Destroy(gameObject);
            }
        }
    }
}
