using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public bool hidden { get; private set; }
    [SerializeField] private bool escondido = false;

    void Start()
    {
        hidden = false;
    }

    public void HideInLocker(bool value)
    {
        hidden = value;
        escondido = value;
    }
}
