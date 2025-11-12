using UnityEngine;
using TMPro;

public class DisplayDial : MonoBehaviour
{
    [SerializeField] private TextMeshPro textDisplay;

    [SerializeField] private Transform dialEntero;
    [SerializeField] private Transform dialDecimal;

    private const float MAX_VALOR_ENTERO = 500f;
    private const float RANGO_GIRO = 360f;

    void Update()
    {
        // Obtener los giros
        float giroEntero = dialEntero.localEulerAngles.y;
        float giroDecimal = dialDecimal.localEulerAngles.y;

        // Normalizar el giro a un valor entre 0.0 y 0.99 (para la decimal) y 0 a 500 (para la entera)
        // Mapeo de 0-360 a 0-9.
        int valorEntero = Mathf.FloorToInt(giroEntero / RANGO_GIRO * MAX_VALOR_ENTERO) % 500;

        // Mapeo de 0-360 a 0-99 (para dos decimales).
        int valorDecimal = Mathf.FloorToInt(giroDecimal / RANGO_GIRO * 100f) % 100;

        // Combinar y Formatear el Texto
        string numeroFormateado = string.Format("{0}.{1:D2}", valorEntero, valorDecimal);
        textDisplay.text = numeroFormateado;
    }
}
