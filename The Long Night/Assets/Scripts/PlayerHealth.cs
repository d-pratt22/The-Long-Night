using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int infectionLevel = 0;
    private int currentHealth;
    private int maxInfectionLevel = 15;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void UseHealingItem(HealingItem healingItem)
    {
        currentHealth += healingItem.healthRestored;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. HP: {currentHealth}");

        if ( currentHealth <= 0 )
        {
            Die();
        }
    }

    public void InfectedByEnemy(int infection)
    {
        infectionLevel += infection;

        if (infectionLevel >= maxInfectionLevel)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("You died");
    }
}
