using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pausePanel;
    public MonoBehaviour playerController;

    void Start()
    {
        if (pausePanel == null)
            Debug.LogError("❌ PausePanel NO ASIGNADO en el inspector.");

        else
            Debug.Log("✔ PausePanel detectado correctamente.");

        pausePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC PRESIONADO");

            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        Debug.Log("▶ Reanudando juego...");

        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        if (playerController != null)
            playerController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        Debug.Log("⏸ Pausando juego...");

        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        if (playerController != null)
            playerController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit game.");
    }
}
