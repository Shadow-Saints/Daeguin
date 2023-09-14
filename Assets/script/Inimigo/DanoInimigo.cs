using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DanoInimigo : MonoBehaviour
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

    private void Die()
    {
        // Adicione aqui a lógica para quando o inimigo morrer (por exemplo, destruir o objeto do inimigo).
        Destroy(gameObject);
    }
}
