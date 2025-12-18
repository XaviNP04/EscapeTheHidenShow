using UnityEngine;

public class ManiquiSalida : MonoBehaviour
{
    [SerializeField] private GameObject[] maniquis;
    [SerializeField] private Transform player;
    [SerializeField] private Animator ascensor;
    private Collider collider;

    void Start()
    {
        collider = player.GetComponent<Collider>();
        ascensor.SetBool("open", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collider == other)
        {
            ascensor.SetBool("open", true);

            for (int i = 0; i < maniquis.Length; i++)
            {
                maniquis[i].GetComponent<ManiquiPerseguir>().Activar();
            }
        }
    }
}
