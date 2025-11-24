using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private Transform _player;

    private PlayerMovement playerMovement;
    private MouseLook mouseLookX;
    private MouseLook mouseLookY;

    void Start()
    {
        playerMovement = _player.GetComponent<PlayerMovement>();

        mouseLookX = _player.GetComponent<MouseLook>();

        Camera playerCamera = _player.GetComponentInChildren<Camera>();
        mouseLookY = playerCamera.GetComponent<MouseLook>();
 
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        if (playerMovement != null)
            playerMovement.enabled = false;
            mouseLookX.enabled = false;
            mouseLookY.enabled = false;

        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;

        if (playerMovement != null)
            playerMovement.enabled = true;
            mouseLookX.enabled = true;
            mouseLookY.enabled = true;
    }

}
