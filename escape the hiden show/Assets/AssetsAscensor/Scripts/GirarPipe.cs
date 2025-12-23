using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GirarPipe : MonoBehaviour, IPointerDownHandler
{
    private bool correcta;
    [SerializeField] private int estadoActual; // 0: 0°, 1: 90°, 2: 180°, 3: 270°
    [SerializeField] private int estadoSol;
    [SerializeField] private bool esRecta; 
    [SerializeField] private bool esSolucion;

    [SerializeField] private PipesManager manager;
    private Quaternion rotacionObjetivo;

    void Start()
    {
        rotacionObjetivo = transform.localRotation;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Girando");
        estadoActual = (estadoActual + 1) % 4;
        rotacionObjetivo = Quaternion.Euler(estadoActual * 90, 0, 0);
        
        if (esSolucion)
        {
            if(esRecta)
            {
                correcta = estadoActual == estadoSol || estadoActual == (estadoSol + 2) % 4;
            }
            else
            {
                correcta = estadoActual == estadoSol;
            }

            if (manager != null)
                manager.comprobar();
        }
    }

    public bool esCorrecta()
    {
        if(correcta)
        {
            Debug.Log("Correcta");
        }
        return correcta;
    }

    void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotacionObjetivo, Time.deltaTime * 10f);
    }
}
