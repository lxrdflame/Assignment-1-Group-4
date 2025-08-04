using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Combat")]
    public float baseDamage = 10f;
    public float fireRate = 0.2f;

    [Header("Movement")]
    public float moveSpeed = 15f;
    public float jumpHeight = 6f;
    public float descentMultiplier = 2.5f; // Gravity multiplier when falling

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Player died.");
        // Implement respawn or game over logic here
    }

    public void ModifyStat(string statName, float amount)
    {
        switch (statName.ToLower())
        {
            case "damage": baseDamage += amount; break;
            case "firerate": fireRate += amount; break;
            case "movespeed": moveSpeed += amount; break;
            case "jumpheight": jumpHeight += amount; break;
            case "descent": descentMultiplier += amount; break;
        }
    }
}