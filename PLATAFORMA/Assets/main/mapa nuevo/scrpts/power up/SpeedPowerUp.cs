using UnityEngine;
using System.Collections;

public class SpeedPowerUp : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.8f;
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement movement = other.GetComponent<PlayerMovement>();

        if (movement != null)
        {
            StartCoroutine(ApplySpeedBuff(movement));

            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }
    }

    private IEnumerator ApplySpeedBuff(PlayerMovement movement)
    {
        movement.SetSpeedMultiplier(speedMultiplier);
        yield return new WaitForSeconds(duration);
        movement.SetSpeedMultiplier(1f);
        Destroy(gameObject);
    }
}