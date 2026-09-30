using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager58 : MonoBehaviour
{
    public enum GameState
    {
        Lobby,
        Jugando,
        Pausa,
        Victoria,
        Derrota
    }

    public GameState EstadoActual { get; private set; }

    private void Awake()
    {
        EstadoActual = GameState.Lobby;
    }

    public void CambiarEstado(GameState nuevoEstado)
    {
        EstadoActual = nuevoEstado;

        Debug.Log("Estado del juego: " + EstadoActual);

        if (EstadoActual == GameState.Pausa)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    // BOTÓN JUGAR
    public void IniciarJuego()
    {
        CambiarEstado(GameState.Jugando);
    }

    // BOTÓN PAUSA
    public void PausarJuego()
    {
        CambiarEstado(GameState.Pausa);
    }

    // BOTÓN CONTINUAR
    public void ContinuarJuego()
    {
        CambiarEstado(GameState.Jugando);
    }

    // VICTORIA
    public void GanarJuego()
    {
        CambiarEstado(GameState.Victoria);
    }

    // DERROTA
    public void PerderJuego()
    {
        CambiarEstado(GameState.Derrota);
    }
}