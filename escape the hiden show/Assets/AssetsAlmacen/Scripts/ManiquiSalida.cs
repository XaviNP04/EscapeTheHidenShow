using UnityEngine;

public class ManiquiSalida : MonoBehaviour
{
    [SerializeField] private GameObject[] maniquis;
    [SerializeField] private Transform player;
    private Collider collider;

    void Start()
    {
        collider = player.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collider == other)
        {
            for (int i = 0; i < maniquis.Length; i++)
            {
                maniquis[i].GetComponent<ManiquiPerseguir>().Activar();
            }
        }
    }
}
