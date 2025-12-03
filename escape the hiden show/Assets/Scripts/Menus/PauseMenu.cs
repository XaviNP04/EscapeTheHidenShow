using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pausePanel;
    public GameObject controlsPanel;  // <--- NUEVO
    public MonoBehaviour playerController;

    void Start()
    {
        pausePanel.SetActive(false);
        controlsPanel.SetActive(false); // <--- OCULTO AL EMPEZAR

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si está en el panel de controles, volver al menú de pausa
            if (controlsPanel.activeSelf)
            {
                CloseControls();
                return;
            }

            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        controlsPanel.SetActive(false);

        Time.timeScale = 1f;
        GameIsPaused = false;

        if (playerController != null)
            playerController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        controlsPanel.SetActive(false);

        Time.timeScale = 0f;
        GameIsPaused = true;

        if (playerController != null)
            playerController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // -----------------------------
    //      CONTROLES
    // -----------------------------
    public void OpenControls()
    {
        controlsPanel.SetActive(true);   // Muestra imagen + botón volver
        pausePanel.SetActive(false);     // Oculta el menú de pausa
    }

    public void CloseControls()
    {
        controlsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    // -----------------------------
    //    BOTONES DEL MENÚ
    // -----------------------------
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
