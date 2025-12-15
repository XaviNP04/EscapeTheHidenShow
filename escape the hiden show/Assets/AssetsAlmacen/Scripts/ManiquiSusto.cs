using UnityEngine;
using UnityEngine.AI;

public class ManiquiSusto : MonoBehaviour
{
    private NavMeshAgent maniqui;
    private Animator animator;
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private Transform punto;
    [SerializeField] private AudioSource caminar;

    void Start()
    {
        maniqui = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void MoverManiqui()
    {
        caminar.Play();
        animator.SetBool("Activo", true);
        maniqui.speed = velocidad;
        maniqui.SetDestination(punto.position);
    }

    void Update()
    {
        if (maniqui.remainingDistance <= maniqui.stoppingDistance)
        {
            caminar.Stop();
            animator.SetBool("Activo", false);
        }
    }
}

