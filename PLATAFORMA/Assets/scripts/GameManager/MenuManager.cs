using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Paneles de la Interfaz")]
    [SerializeField] private GameObject menuPrincipalPanel;
    [SerializeField] private GameObject opcionesPanel;
    [SerializeField] private GameObject pausaPanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Estado del Juego")]
    private bool juegoPausado = false;

    private void Update()
    {
        // Detecta la tecla Escape (o Start en control) para pausar o reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    #region Navegación de Menús
    public void AbrirOpciones()
    {
        if (menuPrincipalPanel != null) menuPrincipalPanel.SetActive(false);
        if (opcionesPanel != null) opcionesPanel.SetActive(true);
    }

    public void CerrarOpciones()
    {
        if (opcionesPanel != null) opcionesPanel.SetActive(false);
        if (menuPrincipalPanel != null) menuPrincipalPanel.SetActive(true);
    }
    #endregion

    #region Control del Flujo de Juego
    public void CargarEscena(string nombreEscena)
    {
        Time.timeScale = 1f; // Asegura que el tiempo vuelva a la normalidad
        SceneManager.LoadScene(nombreEscena);
    }

    public void ReiniciarEscenaActual()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PausarJuego()
    {
        juegoPausado = true;
        Time.timeScale = 0f; // Congela el tiempo del juego
        if (pausaPanel != null) pausaPanel.SetActive(true);
    }

    public void ReanudarJuego()
    {
        juegoPausado = false;
        Time.timeScale = 1f; // Devuelve el tiempo a normal
        if (pausaPanel != null) pausaPanel.SetActive(false);
    }

    public void MostrarGameOver()
    {
        Time.timeScale = 0f;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
    #endregion
}