using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuSceneManager : MonoBehaviour
{

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    public void OnPlayButton()
    {
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
