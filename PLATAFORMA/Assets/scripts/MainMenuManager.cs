using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Configuración de Escenas")]
    [Tooltip("Nombre exacto de la escena del juego principal")]
    [SerializeField] private string nombreEscenaJuego = "Nivel1";

    [Header("Paneles (Opcional)")]
    [SerializeField] private GameObject panelMenuPrincipal;
    [SerializeField] private GameObject panelOpciones;

    private void Start()
    {
        // Aseguramos el estado inicial de los paneles
        if (panelMenuPrincipal != null) panelMenuPrincipal.SetActive(true);
        if (panelOpciones != null) panelOpciones.SetActive(false);
    }

    /// <summary>
    /// Inicia el juego cargando la escena del nivel principal.
    /// </summary>
    public void BotonIniciarJuego()
    {
        // Carga la escena del juego
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    /// <summary>
    /// Abre la ventana de opciones.
    /// </summary>

    public void BotonAbrirOpciones()
    {
        if (panelMenuPrincipal != null) panelMenuPrincipal.SetActive(false);
        if (panelOpciones != null) panelOpciones.SetActive(true);
    }

    /// <summary>
    /// Regresa al menú principal desde las opciones.
    /// </summary>
    public void BotonVolverAlMenu()
    {
        if (panelOpciones != null) panelOpciones.SetActive(false);
        if (panelMenuPrincipal != null) panelMenuPrincipal.SetActive(true);
    }

    /// <summary>
    /// Cierra la aplicación/juego.
    /// </summary>
    public void BotonSalir()
    {
        Debug.Log("Saliendo del juego...");
        
        #if UNITY_EDITOR
            // Si estamos ejecutando desde el editor de Unity
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Si es la compilación final (Build)
            Application.Quit();
        #endif
    }
}