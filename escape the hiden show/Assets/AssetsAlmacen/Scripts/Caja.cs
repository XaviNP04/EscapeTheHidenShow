using UnityEngine;

public class Caja : MonoBehaviour
{
    private bool cerrada = true;
    [SerializeField] private bool correcta = false;
    private Animator animator;
    private Camera _camera;
    [SerializeField] private ActivacionManiquis control;

    void Start()
    {
        _camera = Camera.main;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (cerrada && Input.GetKeyDown(KeyCode.E))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5f))
            {
                var hitObject = hit.transform.gameObject;
                if (hitObject == this.gameObject)
                {
                    cerrada = false;
                    animator.enabled = true;

                    if (!correcta) control.activacionExtra();
                }
            }
        }
    }
}
