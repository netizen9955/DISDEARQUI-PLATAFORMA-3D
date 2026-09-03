using UnityEngine;

public class PoisonZone : MonoBehaviour
{
    public float poisonDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();

        if (player != null)
        {
            player.ApplyPoison(poisonDuration);
        }
    }
}