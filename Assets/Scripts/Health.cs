using System;
using UnityEngine;

/// <summary>
/// HP�ý���
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
        // ���� ü�� �ʱ�ȭ
        currentHealth = maxHealth;
    }

    /// <summary>
    /// HP����
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(float amount)
    {
        HP -= amount;
        HP = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}