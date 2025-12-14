using UnityEngine;

public class CamaraVigilancia : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        // si cambiamos el nombre del jugador cambiar tambien
        player = GameObject.Find("-->FirstPersonPlayer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
    }
}
