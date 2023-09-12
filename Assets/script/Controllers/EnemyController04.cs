using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController04 : MonoBehaviour
{
    public float dashSpeed = 10f; // A velocidade do dash
    public float dashCooldown = 2f; // Tempo de espera entre os dashes
    private bool canDash = true;

    private Rigidbody2D rb;
    private Transform player; // Referência ao Transform do jogador

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Altere a tag conforme o jogador no seu jogo.
    }

    private void Update()
    {
        // Verifique se o inimigo pode realizar o dash
        if (canDash)
        {
            // Calcule a direção do dash em relação à posição do jogador
            Vector2 dashDirection = (player.position - transform.position).normalized;

            // Realize o dash na direção do jogador
            Dash(dashDirection);
        }
    }

    private void Dash(Vector2 direction)
    {
        // Desative a capacidade de dash temporariamente
        canDash = false;

        // Defina a velocidade do Rigidbody2D para a velocidade de dash na direção desejada
        rb.velocity = direction * dashSpeed;

        // Agende a reativação do dash após o tempo de espera
        Invoke("EnableDash", dashCooldown);
    }

    private void EnableDash()
    {
        // Reset a velocidade do Rigidbody2D
        rb.velocity = Vector2.zero;

        // Permita que o inimigo faça dash novamente
        canDash = true;
    }
}
