using ReadyPlayerMe.Core;
using UnityEngine;
using UnityEngine.AI;

public class npcController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public GameObject PATH;
    private Transform[] pathPoints;

    public float minDistance ;

    public int index= 0;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        pathPoints = new Transform[PATH.transform.childCount];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            pathPoints[i] = PATH.transform.GetChild(i);
        }
    }

    void Update()
    {
        roam();
    }

    void roam()
    {
        if(index > 0 && index < pathPoints.Length)
        {
            if (Vector3.Distance(transform.position, pathPoints[index].position) < minDistance)
            {
                index++;
                if (index >= pathPoints.Length)
                {
                    index = 0;
                }
            }
        }
        else
        {
            index = 0;
        }
        
        
        agent.SetDestination(pathPoints[index].position); 
        animator.SetFloat("vertical", !agent.isStopped ? 1 : 0);
    } 
}
