using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Temporizador : MonoBehaviour
{
    [SerializeField] private float tiempoRestante = 300f;
    [SerializeField] private TextMeshPro textoReloj;
    [SerializeField] private Light luz;
    private bool cuentaAtrasActiva;

    [SerializeField] private float fuerzaSacudida = 0.7f;
    [SerializeField] private float duracionImpacto = 1.5f;
    [SerializeField] private Transform camara;
    private Vector3 posCamara;

    [SerializeField] private Image fadeImagen;
    [SerializeField] private float fadeDuracion = 1f;
    [SerializeField] private AudioSource colision;

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

                StartCoroutine(gameOver());
            }
        }
    }

    public void detenerTemporizador()
    {
        cuentaAtrasActiva = false;
    }

    private IEnumerator gameOver()
    {
        Debug.Log("¡Se acabó el tiempo! El ascensor se desploma...");
        colision.Play();

        posCamara = camara.localPosition;
        float tiempoPasado = 0f;
        Color color = fadeImagen.color;

        while (tiempoPasado < duracionImpacto)
        {
            float x = Random.Range(-1f, 1f) * fuerzaSacudida;
            float y = Random.Range(-1f, 1f) * fuerzaSacudida;

            camara.localPosition = new Vector3(x, y, posCamara.z);

            color.a = Mathf.Clamp01(tiempoPasado / fadeDuracion);
            fadeImagen.color = color;

            tiempoPasado += Time.deltaTime;
            yield return null;
        }

        camara.localPosition = posCamara;

        yield return new WaitForSeconds(0.5f);

        string escenaActual = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(escenaActual);
    }
}
