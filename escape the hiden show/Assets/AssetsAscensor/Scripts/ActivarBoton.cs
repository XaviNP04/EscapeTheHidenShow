using UnityEngine;

public class ActivarBoton : MonoBehaviour
{
    private Camera _camera;
    private bool pulsado = false;
    private Material mat;
    [SerializeField] private string valor;
    [SerializeField] private RestablecerAscensor manager;

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
                    pulsado = true;
                    mat.EnableKeyword("_EMISSION");
                    manager.anadirNumero(valor);
                }
            }
        }
    }

    public void reiniciar()
    {
        pulsado = false;
        mat.DisableKeyword("_EMISSION");
    }
}
