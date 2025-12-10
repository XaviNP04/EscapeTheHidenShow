using UnityEngine;
using System.Collections;

public class ActivacionManiquis : MonoBehaviour
{
    [SerializeField] private GameObject[] maniquis;
    [SerializeField] private float timer = 30f;
    [SerializeField] private float rango = 0.5f;
    private ManiquiPerseguir[] perseguir;

    void Start()
    {
        perseguir = new ManiquiPerseguir[maniquis.Length];
        for (int i = 0; i < maniquis.Length; i++)
        {
            perseguir[i] = maniquis[i].GetComponent<ManiquiPerseguir>();
        }

        StartCoroutine(activarManiqui());
    }

    private IEnumerator activarManiqui()
    {
        while(true)
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
