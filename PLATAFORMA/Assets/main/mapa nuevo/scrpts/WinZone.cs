using UnityEngine;

public class WinZone : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject winTextObject;

    [Header("Opciones")]
    [SerializeField] private bool pauseGameOnWin = true;

    private bool hasWon = false; // Evita ejecuciones duplicadas o referencias nulas

    private void OnTriggerEnter(Collider other)
    {
        if (hasWon) return; // Si ya se activó la victoria, no vuelve a ejecutar nada

        if (other.CompareTag("Player") || other.GetComponent<PlayerMovement>() != null)
        {
            hasWon = true;

            if (winTextObject != null)
            {
                winTextObject.SetActive(true);
            }

            if (pauseGameOnWin)
            {
                Time.timeScale = 0f;
            }
        }
    }
}