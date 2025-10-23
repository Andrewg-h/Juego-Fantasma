using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LumaCollect : MonoBehaviour
{
    // UI (TextMeshPro)
    public TextMeshProUGUI azulText;
    public TextMeshProUGUI moradoText;
    public TextMeshProUGUI victoriaText;   // Texto que mostrará "Rescata a Kaede" / "¡Has ganado!"
    public TextMeshProUGUI derrotaText;

    // Item especial (Kaede) - arrastrar la instancia inactiva en la escena
    public GameObject itemEspecial;

    // Panel de victoria (opcional): lo activamos al finalizar
    public GameObject victoryPanel;

    // Contadores
    private int espiritusAzules = 0;
    private int espiritusMorados = 0;

    // Requisitos para que aparezca Kaede
    private int totalAzules = 6;
    private int totalMorados = 4;

    // Tiempo entre mensaje de rescate y mensaje final
    public float delayToWinMessage = 1.5f;

    private void Start()
    {
        // Inicial UI
        if (azulText != null) azulText.text = $"Azules: 0 / {totalAzules}";
        if (moradoText != null) moradoText.text = $"Morados: 0 / {totalMorados}";

        if (victoriaText != null) victoriaText.gameObject.SetActive(false);
        if (derrotaText != null) derrotaText.gameObject.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);

        // Asegurar Kaede oculta al inicio
        if (itemEspecial != null) itemEspecial.SetActive(false);

        // Asegúrate de que el juego corre normalmente
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Espíritus azules
        if (collision.CompareTag("azul"))
        {
            espiritusAzules++;
            if (azulText != null) azulText.text = $"Azules: {espiritusAzules} / {totalAzules}";
            Destroy(collision.gameObject);
        }
        // Espíritus morados
        else if (collision.CompareTag("morado"))
        {
            espiritusMorados++;
            if (moradoText != null) moradoText.text = $"Morados: {espiritusMorados} / {totalMorados}";
            Destroy(collision.gameObject);
        }
        // Si toca Kaede (ítem especial)
        else if (collision.CompareTag("especial"))
        {
            // Destruir Kaede para que no pueda recogerse de nuevo
            Destroy(collision.gameObject);

            // Lanzamos la secuencia de victoria (mensaje -> cambio -> panel -> pausa)
            StartCoroutine(HandleVictorySequence());
        }
        // Si toca enemigo (gato)
        else if (collision.CompareTag("enemigo"))
        {
            if (derrotaText != null)
            {
                derrotaText.gameObject.SetActive(true);
                derrotaText.text = "¡Has perdido!";
            }

            // Reiniciar después de 1.5 segundos (o el delay que prefieras)
            Invoke(nameof(ReiniciarNivel), 1.5f);
        }

        // Si ya recogió los requisitos, mostramos Kaede (solo una vez)
        if (espiritusAzules >= totalAzules && espiritusMorados >= totalMorados)
        {
            if (itemEspecial != null && !itemEspecial.activeSelf)
            {
                itemEspecial.SetActive(true);

                // Mostrar mensaje de misión en pantalla
                if (victoriaText != null)
                {
                    victoriaText.gameObject.SetActive(true);
                    victoriaText.text = "Rescata a Kaede";
                }
            }
        }
    }

    // Secuencia que corre al recoger Kaede
    private IEnumerator HandleVictorySequence()
    {
        // Mensaje inmediato de rescate (puede ya haber aparecido "Rescata a Kaede", lo sobreescribimos)
        if (victoriaText != null)
        {
            victoriaText.gameObject.SetActive(true);
            victoriaText.text = "¡Has rescatado a Kaede!";
        }

        // Pequeña pausa para que el jugador lea
        yield return new WaitForSeconds(delayToWinMessage);

        // Mensaje final de victoria
        if (victoriaText != null)
        {
            victoriaText.text = "¡Has ganado!";
        }

        // Mostrar panel de victoria (botones, stats, etc.)
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        // Pausar el juego para que el jugador no siga moviéndose
        Time.timeScale = 0f;

        // (Opcional) Aquí podrías también desactivar el script de movimiento de Luma:
        // var movement = GetComponent<PlayerMovement>();
        // if (movement != null) movement.enabled = false;
    }

    private void ReiniciarNivel()
    {
        // Antes de reiniciar, asegurarnos de que el tiempo está normal
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Método público para usar en el botón "Reintentar" del VictoryPanel
    public void OnRetryButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}