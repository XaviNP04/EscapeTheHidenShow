using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class MainMenuSceneManager : MonoBehaviour
{

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    [SerializeField] private GameObject creditsPanel;

    private bool isTransitioning = false;

    void Update()
    {
        if (!creditsPanel.activeInHierarchy) {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                OnPlayButton();
       
        } else
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnPlayButton()
    {
        if (isTransitioning) return;

        isTransitioning = true;

        StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene("TutorialRoom");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
