using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Collider attackRadius;
    public int damage = 25;
    public float attackSpeed = 2;
    public int infectionAmount = 1;
    public float infectionChance = 25f;

    private bool canAttack = true;

    public void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (canAttack)
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            playerHealth.TakeDamage(damage);

            if (Random.Range(1, 101) <= infectionChance)
            {
                playerHealth.InfectedByEnemy(infectionAmount);
            }

            StartCoroutine(attackWaitTime());
        }
    }

    IEnumerator attackWaitTime()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}
