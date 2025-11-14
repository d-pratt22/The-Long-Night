using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int infectionLevel = 0;
    private int currentHealth;
    private int maxInfectionLevel = 5;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI infectionText;

    public GameObject gameOverText;
    public float gameOverTime = 5f;

    [Header("Damage Vignette")]
    public Image damageVignette; 
    public float vignetteDuration = 0.5f; 
    private float vignetteTimer = 0f;

    private void Start()
    {
        currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth;
        infectionText.text = "Infection Level: " + infectionLevel;
        damageVignette.color = new Color(1, 0, 0, 0); 
        gameOverText.SetActive(false);
    }

    private void Update()
    {
        if (vignetteTimer > 0)
        {
            vignetteTimer -= Time.deltaTime;
            float alpha = vignetteTimer / vignetteDuration;
            damageVignette.color = new Color(1, 0, 0, alpha);
        }
    }

    public void UseHealingItem(HealingItem healingItem)
    {
        currentHealth += healingItem.healthRestored;
        healthText.text = "Health: " + currentHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. HP: {currentHealth}");
        healthText.text = "Health: " + currentHealth;

        vignetteTimer = vignetteDuration;
        damageVignette.color = new Color(1, 0, 0, 1); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void InfectedByEnemy(int infection)
    {
        infectionLevel += infection;
        infectionText.text = "Infection Level: " + infectionLevel;

        if (infectionLevel >= maxInfectionLevel)
        {
            Die();
        }
    }

    public void Die()
    {
        gameOverText.SetActive(true);

        StartCoroutine(gameOver());
    }

    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(gameOverTime);


        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }
}
