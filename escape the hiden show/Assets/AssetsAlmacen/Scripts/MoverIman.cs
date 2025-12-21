using UnityEngine;
using UnityEngine.EventSystems;

public class MoverIman : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float velocidad;
    [SerializeField] private string movimiento;
    [SerializeField] private Transform brazo;
    [SerializeField] private Transform iman;
    private Material mat;
    private bool moving = false;
    private Vector3 bPos;
    private Vector3 iPos;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        moving = true;
        mat.EnableKeyword("_EMISSION");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moving = false;
        mat.DisableKeyword("_EMISSION");
    }

    void Update()
    {
        if (moving)
        {
            bPos = brazo.position;
            iPos = iman.position;

            switch (movimiento)
            {
                case "arriba":
                    if (bPos.x + velocidad < 14f)
                        bPos.x = bPos.x + velocidad;
                    else
                        bPos.x = 14f;
                    brazo.position = bPos;
                    break;
                case "abajo":
                    if (bPos.x - velocidad > -20f)
                        bPos.x = bPos.x - velocidad;
                    else
                        bPos.x = -20f;
                    brazo.position = bPos;
                    break;
                case "derecha":
                    if (iPos.z - velocidad > -31f)
                        iPos.z = iPos.z - velocidad;
                    else
                        iPos.z = -31f;
                    iman.position = iPos;
                    break;
                case "izquierda":
                    if (iPos.z + velocidad < 1f)
                        iPos.z = iPos.z + velocidad;
                    else
                        iPos.z = 1f;
                    iman.position = iPos;
                    break;
            }
        }
    }
}
