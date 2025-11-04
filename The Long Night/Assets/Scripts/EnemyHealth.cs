using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public GameObject hitEffectPrefab; // Optional particle effect on hit

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. HP: {currentHealth}");

        if (hitEffectPrefab)
        {
            Instantiate(hitEffectPrefab, hitPoint, Quaternion.identity);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        Destroy(gameObject);
    }
}

