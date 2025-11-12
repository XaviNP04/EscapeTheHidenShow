using UnityEngine;

public class ViewPuzzle : MonoBehaviour
{
    private Camera _camera;
    private Transform personaje;
    private PlayerMovement movement;
    private MouseLook mouselook;
    private MouseLook mouselookCam;
    private bool inspecting;

    void Start()
    {
        _camera = GetComponent<Camera>();
        personaje = transform.parent;
        movement = personaje.GetComponent<PlayerMovement>();
        mouselook = personaje.GetComponent<MouseLook>();
        mouselookCam = GetComponent<MouseLook>();
        inspecting = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !inspecting)
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5f))
            {
                var hitObject = hit.transform.gameObject;
                var target = hitObject.GetComponent<ViewedTarget>();
                if (target != null)
                {
                    movement.enabled = false;
                    mouselook.enabled = false;
                    mouselookCam.enabled = false;
                    inspecting = true;
                    target.View();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && inspecting)
        {
            movement.enabled = true;
            mouselook.enabled = true;
            mouselookCam.enabled = true;
            inspecting = false;
        }
    }
}
