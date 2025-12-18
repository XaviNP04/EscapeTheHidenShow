using UnityEngine;
using System.Collections;

public class ActivarAscensor : MonoBehaviour
{
    [SerializeField] private Animator ascensor;
    [SerializeField] private Animator luces;
    [SerializeField] private AudioSource audio;
    [SerializeField] private float espera = 1f;

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            StartCoroutine(cerrandoPuertas());
        }
    }

    private IEnumerator cerrandoPuertas()
    {
        yield return new WaitForSeconds(espera);
        ascensor.SetBool("open", false);
        luces.enabled = true;
        audio.Play();
    }
}
