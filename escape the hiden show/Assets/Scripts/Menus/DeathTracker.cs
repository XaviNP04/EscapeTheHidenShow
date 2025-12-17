using UnityEngine;

public class DeathTracker : MonoBehaviour
{
    public static DeathTracker Instance { get; private set; }

    public int numDeaths { get; private set; }

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void IncrementDeathCount()
    {
        numDeaths++;
    }

    public void Shutdown()
    {
        Instance = null;

        Destroy(gameObject);
    }
}
