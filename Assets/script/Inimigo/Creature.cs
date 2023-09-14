using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Ação a ser executada quando a criatura morrer (pode ser sobrescrita nas classes derivadas)
        Destroy(gameObject);
    }
}
