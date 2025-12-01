using UnityEngine;
using UnityEngine.AI;

public class ManiquiPerseguir : MonoBehaviour
{
    private NavMeshAgent maniqui;
    private Animator animator;
    public bool activo;
    private bool persecucion;
    private bool cambio;
    [SerializeField] private float velocidad = 10.5f;
    [SerializeField] private Transform player;
    private PlayerStats playerStats;
    private Vector3 posicionOriginal;
    [SerializeField] private float rangoBusqueda = 100f;
    [SerializeField] private Transform centroBusqueda;

    void Start()
    {
        posicionOriginal = transform.position;
        maniqui = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerStats = player.GetComponent<PlayerStats>();
        activo = true;
        persecucion = false;
        cambio = false;
    }

    void Update()
    {
        if (activo && !playerStats.hidden)
        {
            maniqui.speed = velocidad;
            persecucion = true;
            animator.SetBool("Activo", activo);
            animator.SetBool("Persecucion", persecucion);
            maniqui.SetDestination(player.position);
        } 
        else if (activo && playerStats.hidden)
        {
            maniqui.speed = velocidad / 2;
            if (persecucion)
            {
                animator.SetBool("Persecucion", false);
                maniqui.SetDestination(posicionOriginal);
            }
            else
            {
                if (!cambio)
                {
                    animator.SetBool("Activo", activo);
                    Vector3 posicionAl = centroBusqueda.position + Random.insideUnitSphere * rangoBusqueda;
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(posicionAl, out hit, rangoBusqueda, NavMesh.AllAreas))
                    {
                        maniqui.SetDestination(hit.position);
                        cambio = true;
                    }
                }
            }
        }

        if (!maniqui.pathPending && maniqui.remainingDistance <= maniqui.stoppingDistance)
        {
            activo = false;
            persecucion = false;
            cambio = false;
            animator.SetBool("Activo", activo);
        }
    }
}
