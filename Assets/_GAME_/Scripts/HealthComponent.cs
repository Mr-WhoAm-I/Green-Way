using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; 
    private int currentHealth;

    public event Action OnDeath;
    public event Action<int> OnHealthChanged; 
    public event Action<int> OnTakeDamage; 

    private void Awake()
    {
        currentHealth = maxHealth; 
    }


    public void TakeDamage(int damage)
    {
        if (damage <= 0) return; 
        currentHealth -= damage;

        OnTakeDamage?.Invoke(damage);
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return; 
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 

        OnHealthChanged?.Invoke(currentHealth);
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = Mathf.Max(1, newMaxHealth);
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SaveHealth()
    {
        PlayerPrefs.SetInt("PlayerMaxHealth", maxHealth);
        PlayerPrefs.SetInt("PlayerCurrentHealth", currentHealth);
        PlayerPrefs.Save();
    }

    public void LoadHealth()
    {
        maxHealth = PlayerPrefs.GetInt("PlayerMaxHealth", 10);
        currentHealth = PlayerPrefs.GetInt("PlayerCurrentHealth", maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
    }
}
