using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public bool hidden { get; private set; }

    void Start()
    {
        hidden = false;
    }

    public void HideInLocker(bool value)
    {
        hidden = value;
    }
}
