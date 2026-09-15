using UnityEngine;
using System.Collections;

public class DoubleJumpPowerUp : MonoBehaviour
{
    [SerializeField] private float duration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement movement = other.GetComponent<PlayerMovement>();

        if (movement != null)
        {
            StartCoroutine(ApplyDoubleJumpBuff(movement));

            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }
    }

    private IEnumerator ApplyDoubleJumpBuff(PlayerMovement movement)
    {
        movement.canDoubleJump = true;
        yield return new WaitForSeconds(duration);
        movement.canDoubleJump = false;
        Destroy(gameObject);
    }
}