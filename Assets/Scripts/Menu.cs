using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject panelCredits;
    public GameObject panelSettings;

    void Start()
    {
        if (panelCredits != null) panelCredits.SetActive(false);
        if (panelSettings != null) panelSettings.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OpenCredits()
    {
        panelCredits.SetActive(true);
    }

    public void CloseCredits()
    {
        panelCredits.SetActive(false);
    }

    public void OpenSettings()
    {
        panelSettings.SetActive(true);
    }

    public void CloseSettings()
    {
        panelSettings.SetActive(false);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
