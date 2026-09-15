using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    public int healAmount = 30;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();

        if (player != null)
        {
            player.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}