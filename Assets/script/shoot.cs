using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    [SerializeField] private float _speed; // Velocidade do tiro
    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime); // Movimento do tiro
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique se a bala colidiu com um objeto que deve receber dano (por exemplo, um inimigo)
        if (other.CompareTag("Enemy"))
        {
            // Obtenha o componente EnemyController do objeto
            EnemyController enemy = other.GetComponent<EnemyController>();

            if (enemy != null)
            {
                // Destrua a bala após causar dano
                Destroy(gameObject);
            }
        }
    }
}
