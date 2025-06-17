using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    [Header("Path Settings")]
    public GameObject pathObject;
    public float minDistance = 1f;

    private Transform[] pathPoints;
    private int currentIndex;

    private void Awake()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponent<Animator>();

        InitializePath();
    }

    private void Update()
    {
        Roam();
    }

    private void InitializePath()
    {
        int childCount = pathObject.transform.childCount;
        pathPoints = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            pathPoints[i] = pathObject.transform.GetChild(i);
        }
    }

    private void Roam()
    {
        if (pathPoints.Length == 0) return;

        float distance = Vector3.Distance(transform.position, pathPoints[currentIndex].position);
        if (distance < minDistance)
        {
            currentIndex = (currentIndex + 1) % pathPoints.Length;
        }

        agent.SetDestination(pathPoints[currentIndex].position);
        animator.SetFloat("vertical", agent.velocity.magnitude > 0.1f ? 1f : 0f);
    }
}
