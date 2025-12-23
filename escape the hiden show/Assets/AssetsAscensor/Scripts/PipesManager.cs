using UnityEngine;

public class PipesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] luces;
    [SerializeField] private GameObject screwdriver;
    [SerializeField] private GirarPipe[] pipes;

    public void comprobar()
    {
        bool resuelto = true;
        Debug.Log("Comprobando");
        foreach (GirarPipe pipe in pipes)
        {
            if (!pipe.esCorrecta())
            {
                resuelto = false;
                break;
            }
        }

        if (resuelto)
        {
            Debug.Log("Resuelto");
            resolver();
        }
    }

    private void resolver()
    {
        foreach (GameObject luz in luces)
        {
            luz.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }
        screwdriver.GetComponent<Collider>().enabled = true;
        screwdriver.GetComponent<Rigidbody>().isKinematic = false;
    }
}
