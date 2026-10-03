using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Necesario para la carga de escenas
using TMPro;

public class LoadingManager : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string sceneName = "GameScene";

    [Header("Referencias de UI")]
    public GameObject mainMenu;
    public GameObject loadingUI;
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;

    void Start()
    {
        if (loadingText == null)
        {
            loadingText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void BeginLoading()
    {
        if (mainMenu != null) mainMenu.SetActive(false);
        if (loadingUI != null) loadingUI.SetActive(true);

        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadOperation.allowSceneActivation = false;

        while (!loadOperation.isDone)
        {
            // Unity normaliza el progreso de 0 a 0.9 mientras allowSceneActivation es false.
            // Dividir entre 0.9f convierte la escala a 0 - 1 (0% a 100%).
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);

            if (loadingBar != null)
            {
                loadingBar.value = progress;
            }

            if (loadingText != null)
            {
                loadingText.text = (progress * 100f).ToString("F0") + "%";
            }

            // Cuando el progreso alcanza el 90% de la carga real (0.9f en Unity):
            if (loadOperation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.5f); // Pequeña pausa opcional
                loadOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}