using UnityEngine;

public class Locker : MonoBehaviour
{
    private Collider interior;
    [SerializeField] private GameObject door;
    private bool isClosed;
    private Animator animatorDoor;
    [SerializeField] private bool playerIn;
    private PlayerStats stats;

    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        animatorDoor = door.GetComponent<Animator>();
        isClosed = animatorDoor.GetBool("Closed");
    }

    private void OnTriggerEnter(Collider other)
    {
        stats = other.GetComponent<PlayerStats>();
        if (stats != null )
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        stats = other.GetComponent<PlayerStats>();
        if (stats != null)
        {
            playerIn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5f))
            {
                var hitObject = hit.transform.gameObject;
                if (hitObject == door)
                {
                    isClosed = !isClosed;
                    animatorDoor.SetBool("Closed", isClosed);
                }
            }
        }

        if (playerIn)
        {
            if (isClosed)
                stats.HideInLocker(true);
            else
                stats.HideInLocker(false);
        }
    }
}
