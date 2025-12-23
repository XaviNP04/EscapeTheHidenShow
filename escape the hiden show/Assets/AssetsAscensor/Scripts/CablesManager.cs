using UnityEngine;
using System.Collections;

public enum TipoColor { Rojo, Blanco, Negro, Azul, Verde, Amarillo, Naranja, Rosa, Morado, Cian }

public class CablesManager : MonoBehaviour
{
    [SerializeField] private AbrirShield shield;
    [SerializeField] private GameObject[] luces; 
    [SerializeField] private ConectarCable[] cables;
    [SerializeField] private TipoColor[] secuenciaCorrecta;
    private TipoColor[] cablesConectados;
    private int cont = 0;

    void Start()
    {
        cablesConectados = new TipoColor[secuenciaCorrecta.Length];
    }

    public void comprobar(TipoColor c)
    {
        cablesConectados[cont] = c;
        cont++;

        if(cont == secuenciaCorrecta.Length)
        {
            for (int i = 0; i < cont; i++)
            {
                if (cablesConectados[i] != secuenciaCorrecta[i])
                {
                    cont = 0;
                    break;
                }
            }

            StartCoroutine(operar());

        }
    }

    private IEnumerator operar()
    {
        yield return new WaitForSeconds(0.5f);
        if (cont == 0)
        {
            foreach (ConectarCable cable in cables)
            {
                cable.reiniciar();
            }
        }
        else
        {
            shield.activar();

            foreach (GameObject luz in luces)
            {
                luz.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            }
        }
    }
}
