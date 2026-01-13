using UnityEngine;
using TMPro;

public class Temporizador : MonoBehaviour
{
    [SerializeField] private float tiempoRestante = 300f;
    [SerializeField] private TextMeshPro textoReloj;
    [SerializeField] private Light luz;
    private bool cuentaAtrasActiva;

    void Start()
    {
        cuentaAtrasActiva = true;
    }

    void Update()
    {
        if (cuentaAtrasActiva)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;

                float minutos = Mathf.FloorToInt(tiempoRestante / 60);
                float segundos = Mathf.FloorToInt(tiempoRestante % 60);

                textoReloj.text = string.Format("{0:00}:{1:00}", minutos, segundos);

                if (tiempoRestante < 60)
                {
                    luz.color = new Color(140 / 255f, 70 / 255f, 70 / 255f);
                }
            }
            else
            {
                tiempoRestante = 0;
                textoReloj.text = string.Format("{0:00}:{1:00}", 0, 0);
                cuentaAtrasActiva = false;
                gameOver();
            }
        }
    }


    void gameOver()
    {
        Debug.Log("¡Se acabó el tiempo! El ascensor se desploma...");
    }

    public void detenerTemporizador()
    {
        cuentaAtrasActiva = false;
    }
}
