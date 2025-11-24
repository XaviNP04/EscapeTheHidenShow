using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GoalPlatform : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            StartCoroutine(TransitionRoutine());
        }
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

        SceneManager.LoadScene(2);
    }
}
