using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ManiquiPerseguir : MonoBehaviour
{
    private NavMeshAgent maniqui;
    private Animator animator;
    public bool activo;
    [SerializeField] private bool persecucion;
    [SerializeField] private bool cambio;
    [SerializeField] private float velocidad = 10.5f;
    [SerializeField] private bool escondido;
    [SerializeField] private Transform player;
    private PlayerStats playerStats;
    private Vector3 posicionOriginal;
    [SerializeField] private float rangoBusqueda = 100f;
    [SerializeField] private Transform centroBusqueda;

    [SerializeField] private AudioSource correr;
    [SerializeField] private AudioSource caminar;

    [SerializeField] private AudioSource screamSource;

    void Start()
    {
        posicionOriginal = transform.position;
        maniqui = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerStats = player.GetComponent<PlayerStats>();
        activo = false;
        persecucion = false;
        cambio = false;
    }

    void Update()
    {
        if (activo && !playerStats.hidden)
        {
            if (!correr.isPlaying)
            {
                correr.Play();
                caminar.Stop();
            }
            escondido = playerStats.hidden;
            maniqui.speed = velocidad;
            persecucion = true;
            animator.SetBool("Activo", activo);
            animator.SetBool("Persecucion", persecucion);
            maniqui.SetDestination(player.position);
        } 
        else if (activo && playerStats.hidden)
        {
            if (!caminar.isPlaying)
            {
                correr.Stop();
                caminar.Play();
            }
            escondido = playerStats.hidden;
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
            caminar.Stop();
            correr.Stop();

            activo = false;
            persecucion = false;
            cambio = false;
            animator.SetBool("Activo", activo);
            animator.SetBool("Persecucion", persecucion);
        }
    }

    public void Activar()
    {
        activo = true;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerStats stats = other.GetComponent<PlayerStats>();
        if(stats != null)
        {
            screamSource.Play();
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
