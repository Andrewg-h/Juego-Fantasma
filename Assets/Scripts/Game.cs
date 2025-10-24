using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject instructionsPanel;
    public GameObject pausePanel;
    public GameObject settingsPanel; // El mismo estilo de panel dummy del menú

    private bool isPaused = false;

    void Start()
    {
        // Pausa el juego al iniciar y muestra las instrucciones
        Time.timeScale = 0f;
        instructionsPanel.SetActive(true);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    // ---- PANEL DE INSTRUCCIONES ----
    public void StartGame()
    {
        instructionsPanel.SetActive(false);
        Time.timeScale = 1f; // Reanuda el juego
    }

    // ---- PAUSA ----
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // ---- CONFIGURACIÓN ----
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // ---- MENÚ PRINCIPAL ----
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // Cambia "Menu" por el nombre exacto de tu escena de menú
    }
}