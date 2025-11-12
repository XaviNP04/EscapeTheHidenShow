using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class SpeakerPuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject requiredObject1;
    [SerializeField] private GameObject requiredObject2;
    [SerializeField] private GameObject requiredObject3;
    private Rigidbody rb;

    [SerializeField] private TextMeshPro textDisplay;
    [SerializeField] private float freqCorrecta = 100.55f;
    private float freqDisplay;

    [SerializeField] private DisplayDial displayScript;

    static private bool resuelto = false;

    void Start()
    {
        requiredObject2.SetActive(false);

        rb = requiredObject3.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !resuelto)
        {
            HandleComponentClick();
        }
    }

    private void HandleComponentClick()
    {
        // Lanza un rayo desde la posición del ratón en la pantalla.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5f))
        {
            var hitObject = hit.transform.gameObject;

            if (hit.transform.gameObject == this.gameObject)
            {
                freqDisplay = float.Parse(textDisplay.text, CultureInfo.InvariantCulture);

                if(freqDisplay == freqCorrecta)
                {
                    displayScript.enabled = false;
                    textDisplay.text = "Correcto";
                    StartCoroutine(RomperCaja());

                    Debug.Log("Algo se resquebraja");
                    resuelto = true;
                } else
                {
                    Debug.Log("No ha ocurrido nada");
                }
            }
        }
    }

    private IEnumerator RomperCaja()
    {
        yield return new WaitForSeconds(3f);
        Destroy(requiredObject1);
        requiredObject2.SetActive(true);
        rb.isKinematic = false;
    }
}
