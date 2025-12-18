using UnityEngine;
using System.Collections;

public class ActivacionManiquis : MonoBehaviour
{
    [SerializeField] private GameObject[] maniquis;
    [SerializeField] private float timer = 30f;
    [SerializeField] private float rango = 0.5f;
    private ManiquiPerseguir[] perseguir;

    private bool sistemaActivo = true; 
    private Coroutine bucleActivacion;

    void Start()
    {
        perseguir = new ManiquiPerseguir[maniquis.Length];
        for (int i = 0; i < maniquis.Length; i++)
        {
            perseguir[i] = maniquis[i].GetComponent<ManiquiPerseguir>();
        }

        bucleActivacion = StartCoroutine(activarManiqui());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            sistemaActivo = !sistemaActivo;

            if(sistemaActivo)
            {
                if (bucleActivacion == null)
                {
                    bucleActivacion = StartCoroutine(activarManiqui());
                    Debug.Log("Sistema de maniquís activado");
                }
            }
            else
            {
                if (bucleActivacion != null)
                {
                    StopCoroutine(bucleActivacion); 
                    bucleActivacion = null;
                    Debug.Log("Sistema de maniquís desactivado");
                }
            }
        }
    }

    private IEnumerator activarManiqui()
    {
        while(sistemaActivo)
        {
            float espera = timer + Random.Range(-rango, rango);
            yield return new WaitForSeconds(espera);
            var index = Random.Range(0, maniquis.Length);
            perseguir[index].Activar();
        }
        
    }

    public void activacionExtra()
    {
        var index = Random.Range(0, maniquis.Length);
        perseguir[index].Activar();
        Debug.Log("EQUIVOCADA");
    }
}
