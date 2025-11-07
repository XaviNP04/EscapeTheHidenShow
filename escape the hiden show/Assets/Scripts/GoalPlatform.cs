using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPlatform : MonoBehaviour
{
    private Renderer platformRenderer;
    public Color normalColor = Color.white;
    public Color playerOnColor = Color.green;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            platformRenderer.material.color = playerOnColor;
            SceneManager.LoadScene(2);
        }

        if (other.CompareTag("Player"))
        {
            platformRenderer.material.color = playerOnColor;
            SceneManager.LoadScene(2);
        }
    }
}
