using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pausePanel;
    public GameObject controlsPanel;
    public MonoBehaviour playerController;
    public GameObject slotsContainer;

    public Camera camera;
    private InspectObject inspectObject;
    private ViewPuzzle viewPuzzle;


    void Start()
    {
        pausePanel.SetActive(false);
        controlsPanel.SetActive(false);

        inspectObject = camera.GetComponent<InspectObject>();
        viewPuzzle = camera.GetComponent<ViewPuzzle>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !inspectObject.inspecting && !viewPuzzle.inspecting)
        {
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
        slotsContainer.SetActive(true);

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
        slotsContainer.SetActive(false);

        Time.timeScale = 0f;
        GameIsPaused = true;

        if (playerController != null)
            playerController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void OpenControls()
    {
        controlsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void CloseControls()
    {
        controlsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }


    public void Restart()
    {
        Time.timeScale = 1f;
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Resume();
        SceneManager.LoadScene("MainMenu");
    }
}
