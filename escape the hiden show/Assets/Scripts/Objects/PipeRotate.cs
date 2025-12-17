using System.Collections;
using UnityEngine;

public class PipeRotate : MonoBehaviour
{
    // en teoria deberia funcionar para rotar el objeto 90 grados, pero al estar en el modo de jugar no se rota el objeto, se rota la mesh o algo parecido
    private Camera playerCamera;
    public float interactDistance = 3f;
    public float rotationDuration = 1f;
    private bool isRotating = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            playerCamera = Camera.main;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDistance))
            {
            print("Click detected");
                if (hit.transform.gameObject == this.gameObject)
                {
                    if (!isRotating)
                    {
                        StartCoroutine(RotateX90());
                    }
                }
            }

        }
    }

    private IEnumerator RotateX90()
    {
        isRotating = true;
        Quaternion start = transform.rotation;
        Quaternion end = start * Quaternion.Euler(90f, 0f, 0f);
        float elapsed = 0f;
        while (elapsed < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(start, end, elapsed / rotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = end;
        isRotating = false;
    }
}
