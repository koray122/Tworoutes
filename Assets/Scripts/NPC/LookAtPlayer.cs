using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;
    public float lookRadius = 5f;
    public float lookSpeed = 5f;

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= lookRadius)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);
        }
    }
}
