using System;
using UnityEngine;

/// <summary>
/// HP시스템
/// </summary>
public class Health : MonoBehaviour, IDamageable
{
    private float currentHealth;
    public float maxHealth;

    public Action onDie;

    public float HP
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;

            currentHealth = Mathf.Min(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                onDie?.Invoke();
            }
        }
    }

    private void OnEnable()
    {
        // 시작 체력 초기화
        currentHealth = maxHealth;
    }

    /// <summary>
    /// HP감소
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(float amount)
    {
        HP -= amount;
        HP = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}