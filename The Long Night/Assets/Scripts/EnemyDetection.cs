using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public EnemyMovement movement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            movement.SetPlayerInRadius(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            movement.SetPlayerInRadius(false);
    }
}
