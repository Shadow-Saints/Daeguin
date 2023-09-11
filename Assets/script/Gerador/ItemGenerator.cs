using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Lista de prefabs de itens a serem gerados
    public float spawnChance = 0.5f; // Chance de gerar um item (0 a 1)

    private PlayerController playerController; // Referência ao PlayerController

    private void Start()
    {
        // Encontre o PlayerController no início
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pedro"))
        {
            GameController.instance.Damege(5);
            playerController.Invencible(1);

            // Verifique se um item deve ser gerado com base na chance
            if (Random.value < spawnChance)
            {
                // Escolha um prefab de item aleatório da lista
                GameObject randomItemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

                // Instancie o item no local da colisão
                Instantiate(randomItemPrefab, collision.transform.position, Quaternion.identity);
            }
        }
    }
}