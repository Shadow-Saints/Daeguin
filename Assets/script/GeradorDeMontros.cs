using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeradorDeMontros : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // Lista de prefabs dos inimigos
    public float spawnInterval = 5f; // Intervalo de tempo entre a geração de monstros
    public Transform[] spawnPoint; // O ponto onde o monstro será gerado

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

        // Crie uma instância do inimigo no ponto de geração
        Instantiate(enemyPrefab, spawnPoint[Random.Range(0,spawnPoint.Length)].position, Quaternion.identity);
    }
}