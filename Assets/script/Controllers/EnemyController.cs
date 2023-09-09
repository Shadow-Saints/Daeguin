using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Velocidade de movimento do inimigo

    private Rigidbody2D rb;
    private Vector2 randomDirection;
    private Camera mainCamera;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        // Calcula os limites da tela com base na posição da câmera
        CalculateScreenBounds();

        // Inicie o movimento aleatório
        GenerateRandomDirection();
    }

    void Update()
    {
        // Move o inimigo na direção aleatória
        rb.velocity = randomDirection * moveSpeed;

        // Verifica se o inimigo atingiu os limites da tela
        if (transform.position.x < minX || transform.position.x > maxX || transform.position.y < minY || transform.position.y > maxY)
        {
            // Se atingir os limites, gera uma nova direção aleatória
            GenerateRandomDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifique se o inimigo colidiu com um obstáculo
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Pare o movimento aleatório e gere uma nova direção
            GenerateRandomDirection();
        }
    }

    private void CalculateScreenBounds()
    {
        // Obtém a posição da câmera em relação ao mundo
        Vector3 cameraPosition = mainCamera.transform.position;

        // Calcula os limites da tela usando a câmera e o tamanho da tela
        minX = cameraPosition.x - mainCamera.orthographicSize * mainCamera.aspect;
        maxX = cameraPosition.x + mainCamera.orthographicSize * mainCamera.aspect;
        minY = cameraPosition.y - mainCamera.orthographicSize;
        maxY = cameraPosition.y + mainCamera.orthographicSize;
    }

    private void GenerateRandomDirection()
    {
        // Gera uma nova direção aleatória
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}