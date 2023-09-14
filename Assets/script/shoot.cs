using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    [SerializeField] private float _speed; // Velocidade da bala
    [SerializeField] private List<string> validEnemyTags = new List<string>(); // Lista de tags de inimigos válidos
    [SerializeField] private int damage = 25; // Dano da bala

    private void Update()
    {
        // Move a bala
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique se a tag do objeto colidido está na lista de tags de inimigos válidos
        if (validEnemyTags.Contains(other.tag))
        {
            // A tag do objeto colidido está na lista, então causa dano ao inimigo (se aplicável)
            DanoInimigo enemy = other.GetComponent<DanoInimigo>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }

        // Destrua a bala após causar dano ou colidir com qualquer objeto
        
    }
}
