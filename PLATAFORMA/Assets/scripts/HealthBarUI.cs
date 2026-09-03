using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBarFill; // Asigna el Fill de la barra
    [SerializeField] private PlayerStats playerStats; // Referencia a los stats del jugador

    private void OnEnable()
    {
        
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (playerStats != null && healthBarFill != null)
        {
            // Normaliza la vida actual entre 0 y 1 para la barra de UI
            healthBarFill.fillAmount = (float)playerStats.currentHealth / playerStats.maxHealth;
        }
    }
}