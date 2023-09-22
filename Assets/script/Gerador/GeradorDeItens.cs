using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDeItens : MonoBehaviour
{
    public List<GameObject> itensPrefabs; // Lista de prefabs dos itens

    public void SpawnItens(Transform spawnPoint)
    {
        // Escolha aleatoriamente um iten da lista
        int randomIndex = Random.Range(0, itensPrefabs.Count);
        GameObject enemyPrefab = itensPrefabs[randomIndex];

        // Crie uma instância do iten no ponto de geração
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}