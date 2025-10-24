using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject creditPanel;
    public GameObject settingsPanel;

    // Inicia el juego
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); 
    }

    // Créditos
    public void OpenCredits()
    {
        creditPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditPanel.SetActive(false);
    }

    // Configuración
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // Salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
