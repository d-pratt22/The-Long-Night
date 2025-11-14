using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerPos;
    NavMeshAgent agent;

    private bool isInRadius = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetPlayerInRadius(bool value)
    {
        isInRadius = value;
    }

    private void Update()
    {
        if (isInRadius)
        {
            agent.destination = playerPos.position;
        }
        else
        {
            agent.destination = transform.position; 
        }
    }
}
