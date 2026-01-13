using UnityEngine;
using System.Collections;

public class Parpadeo : MonoBehaviour
{
    [SerializeField] private Light luz;
    private Material mat;
    [SerializeField] private float intensidadMax = 1.5f;
    [SerializeField] private float probParpadeo = 0.9f;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        StartCoroutine(bucleParpadeo());
    }

    IEnumerator bucleParpadeo()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));

            int rafagas = Random.Range(3, 10);
            for (int i = 0; i < rafagas; i++)
            {
                bool encendido = !luz.enabled;
                setLuz(encendido);

                yield return new WaitForSeconds(Random.Range(0.2f, 0.45f));
             }
             setLuz(true);
        }
    }

    void setLuz(bool estado)
    {
        luz.enabled = estado;
        if (mat != null)
        {
            if (estado) mat.EnableKeyword("_EMISSION");
            else mat.DisableKeyword("_EMISSION");
        }
    }
}
