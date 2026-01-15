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

            float mov = velocidad * Time.deltaTime;

            switch (movimiento)
            {
                case "arriba":
                    bPos.x = Mathf.MoveTowards(bPos.x, 14f, mov);
                    brazo.position = bPos;
                    break;
                case "abajo":
                    bPos.x = Mathf.MoveTowards(bPos.x, -20f, mov);
                    brazo.position = bPos;
                    break;
                case "derecha":
                    iPos.z = Mathf.MoveTowards(iPos.z, -31f, mov);
                    iman.position = iPos;
                    break;
                case "izquierda":
                    iPos.z = Mathf.MoveTowards(iPos.z, 1f, mov);
                    iman.position = iPos;
                    break;
            }
        }
    }
}
