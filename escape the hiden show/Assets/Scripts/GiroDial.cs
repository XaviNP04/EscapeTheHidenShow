using UnityEngine;
using UnityEngine.EventSystems;

public class GiroDial : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public float sensibilidadGiro = 1f;

    private float anguloInicialRaton;
    private Quaternion rotacionInicialRueda;

    [SerializeField] Vector3 ejeRotacion;
    private Camera camara;

    void Start()
    {
        camara = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Funciona");

        rotacionInicialRueda = transform.localRotation;

        // Obtenemos el ángulo inicial del ratón
        anguloInicialRaton = CalcularAnguloRaton(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Funciona");
        // Calcular el ángulo actual del ratón
        float anguloActualRaton = CalcularAnguloRaton(eventData.position);

        // Calcular la diferencia entre el ángulo actual y el ángulo inicial
        float diferenciaAngulo = anguloActualRaton - anguloInicialRaton;

        // Aplicar la rotación
        // Multiplicamos la rotación inicial por el nuevo giro angular.
        // Usamos sensibilidadGiro para ajustar la "fricción".
        Quaternion giroAdicional = Quaternion.AngleAxis(diferenciaAngulo * sensibilidadGiro, ejeRotacion);

        transform.localRotation = rotacionInicialRueda * giroAdicional;
    }

    private float CalcularAnguloRaton(Vector2 posicionPantalla)
    {
        // Proyectar el centro del objeto 3D a la pantalla 2D.
        Vector3 centroEnPantalla = camara.WorldToScreenPoint(transform.position);

        // Calcular el vector desde el centro de la rueda hasta la posición del ratón.
        Vector3 vectorRaton = new Vector3(posicionPantalla.x, posicionPantalla.y, 0) - centroEnPantalla;

        // Usar Mathf.Atan2 para obtener el ángulo del vector.
        // Atan2 devuelve el ángulo en radianes, lo convertimos a grados.
        // El -Mathf.Rad2Deg se usa a menudo para corregir la dirección de giro (horario/antihorario).
        return -Mathf.Atan2(vectorRaton.y, vectorRaton.x) * Mathf.Rad2Deg;
    }
}
