using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class GameManager : MonoBehaviour
{
    [Header("Configuración de Gemas")]
    [Tooltip("Cantidad total de gemas requeridas para abrir la puerta")]
    public int totalGemas = 5;
    
    [SerializeField]
    private int gemasRecolectadas = 0;

    [Header("Referencias de la Escena")]
    [Tooltip("Arrastra aquí el componente TextMeshProUGUI que muestra el contador")]
    public TextMeshProUGUI textoGemas;

    [Tooltip("Arrastra aquí el GameObject de la puerta que bloquea el nivel 2")]
    public GameObject puerta;

    void Start()
    {
        // Actualizamos la interfaz al iniciar el juego (ej. "Gemas: 0 / 5")
        ActualizarTextoGemas();
        Debug.Log("GameManager iniciado. Objetivo: " + totalGemas + " gemas.");
    }

    /// <summary>
    /// Función pública que llaman las gemas o Soroar al recoger una gema.
    /// </summary>
    public void SumarGema()
    {
        gemasRecolectadas++;
        Debug.Log("¡Gema recolectada! Llevas: " + gemasRecolectadas + " / " + totalGemas);

        ActualizarTextoGemas();

        // Verificar si se alcanzó la meta
        if (gemasRecolectadas >= totalGemas)
        {
            AbrirPuerta();
        }
    }

    void ActualizarTextoGemas()
    {
        if (textoGemas != null)
        {
            textoGemas.text = "Gemas: " + gemasRecolectadas + " / " + totalGemas;
        }
        else
        {
            Debug.LogWarning("GameManager: No has asignado el componente de Texto (TextMeshProUGUI) en el Inspector.");
        }
    }

    void AbrirPuerta()
    {
        if (puerta != null)
        {
            Debug.Log("¡Todas las gemas recolectadas! Desactivando la puerta para abrir paso a la torre...");
            puerta.SetActive(false); // Oculta la puerta y elimina su colisión física
        }
        else
        {
            Debug.LogError("GameManager: ¡No asignaste el GameObject de la Puerta en el Inspector!");
        }
    }
}