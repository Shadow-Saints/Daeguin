using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeMontros : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // Lista de prefabs dos inimigos
    public float spawnInterval = 5f; // Intervalo de tempo entre a geração de monstros
    public List<Transform> spawnPoints; // Lista de pontos onde os monstros serão gerados

    private float nextSpawnTime = 0f;

    void Update()
    {
        // Verifique se é hora de gerar um novo monstro
        if (Time.time >= nextSpawnTime)
        {
            SpawnMonster();
            nextSpawnTime = Time.time + spawnInterval; // Defina o próximo momento de geração
        }
    }

    void SpawnMonster()
    {
        // Escolha aleatoriamente um inimigo da lista
        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        // Escolha aleatoriamente um ponto de spawn da lista
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

        // Crie uma instância do inimigo no ponto de geração
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}