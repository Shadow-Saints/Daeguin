using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController02 : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Velocidade de movimento do inimigo
    private Transform target; // Referência ao jogador 
    void Start()
    {
        // Encontre a transformação do jogador
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            // Calcula a direção do jogador
            Vector3 direction = target.position - transform.position;
            direction.Normalize(); // Normaliza para manter a velocidade constante

            // Move o inimigo na direção do jogador
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
