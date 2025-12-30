using UnityEngine;

public class RestablecerAscensor : MonoBehaviour
{
    private Camera _camera;
    private bool pulsado = false;
    private Material mat;
    [SerializeField] private ActivarBoton[] botones;
    [SerializeField] private string codigoCorrecto = "7458";
    [SerializeField] private string codigo = "";

    void Start()
    {
        _camera = Camera.main;
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (!pulsado && Input.GetKeyDown(KeyCode.E))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1f))
            {
                var hitObject = hit.transform.gameObject;
                if (hitObject == this.gameObject)
                {
                    mat.EnableKeyword("_EMISSION");
                    if (codigo == codigoCorrecto)
                    {
                        Debug.Log("Correcto");
                        pulsado = true;
                    } 
                    else
                    {
                        mat.DisableKeyword("_EMISSION");
                        foreach (ActivarBoton b in botones)
                        {
                            b.reiniciar();
                        }
                    }
                }
            }
        }
    }

    public void anadirNumero(string num)
    {
        codigo += num;
    }
}
