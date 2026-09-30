using UnityEngine;

public class Gema : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que entró en el Trigger tiene el Tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Busca el GameManager en la escena y suma la gema
            GameManager gm = FindFirstObjectByType<GameManager>();
            if (gm != null)
            {
                gm.SumarGema();
            }

            // Destruye la gema al recogerla
            Destroy(gameObject);
        }
    }
}