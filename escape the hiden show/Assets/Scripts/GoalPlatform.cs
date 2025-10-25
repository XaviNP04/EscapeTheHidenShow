using UnityEngine;

public class GoalPlatform : MonoBehaviour
{
    private Renderer platformRenderer;
    public Color normalColor = Color.white;
    public Color playerOnColor = Color.green;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformRenderer.material.color = normalColor;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platformRenderer.material.color = playerOnColor;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            platformRenderer.material.color = normalColor;
        }
    }
}
