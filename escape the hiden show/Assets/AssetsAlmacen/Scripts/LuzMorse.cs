using UnityEngine;
using System.Collections;

public class LuzMorse : MonoBehaviour
{
    [SerializeField] private Light luzMorse;
    [SerializeField] private float duracionPunto;
    private float[] secuencia1;
    private float[] secuencia2;
    private float[] secuencia3;
    private float[] secuencia4;

    private float[][] codigo;
    [SerializeField] private Material mat;

    void Start()
    {
        secuencia1 = new float[] { duracionPunto, duracionPunto * 3, duracionPunto * 3, duracionPunto * 3, duracionPunto * 3 };
        secuencia2 = new float[] { duracionPunto * 3, duracionPunto, duracionPunto, duracionPunto, duracionPunto };
        secuencia3 = new float[] { duracionPunto * 3, duracionPunto * 3, duracionPunto * 3, duracionPunto, duracionPunto };
        secuencia4 = new float[] { duracionPunto, duracionPunto, duracionPunto * 3, duracionPunto * 3, duracionPunto * 3 };

        codigo = new float[][] { secuencia1, secuencia2, secuencia3, secuencia4 };

        luzMorse.enabled = false;
        mat = GetComponent<Renderer>().material;
        StartCoroutine(EsperaComienzo(5f));
    }

    private IEnumerator SecuenciaMorse()
    {
        while (true)
        {
            for (int i = 0; i < codigo.Length; i++)
            {
                for (int j = 0; j < codigo[i].Length; j++)
                {
                    float duracionEncendido = codigo[i][j];
                    luzMorse.enabled = true;
                    yield return new WaitForSeconds(duracionEncendido);

                    luzMorse.enabled = false;
                    yield return new WaitForSeconds(duracionPunto);
                }

                yield return new WaitForSeconds(duracionPunto * 2);
            }

            mat.DisableKeyword("_EMISSION");
            yield return new WaitForSeconds(duracionPunto * 6);
            mat.EnableKeyword("_EMISSION");
        }
    }

    private IEnumerator EsperaComienzo(float duracion)
    {
        yield return new WaitForSeconds(duracion);
        StartCoroutine(SecuenciaMorse());
    }
}
