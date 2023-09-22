using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DanoInimigo : MonoBehaviour
{
    private GeradorDeItens _itenGenerator;

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        _itenGenerator = FindAnyObjectByType<GeradorDeItens>();
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tiro")) 
        {
            currentHealth -= 25;
            Debug.Log(this.gameObject.name + currentHealth.ToString());
        }
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
        if (_itenGenerator != null) 
        {
            _itenGenerator.SpawnItens(this.gameObject.transform);
        }
        Destroy(gameObject);
    }
}
