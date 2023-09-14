using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController1 : MonoBehaviour
{
    [SerializeField]
    private int roomLink;

    private bool playerInside = false; // Flag para rastrear se o jogador está dentro da área

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        // Verifique se o jogador está dentro e pressionou uma tecla (por exemplo, "E") para mudar de sala.
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            ChangeRoom();
        }
    }

    private void ChangeRoom()
    {
        // Certifique-se de que GameController.instance não seja nulo antes de chamar a função ChangeScene.
        if (GameController.instance != null)
        {
            // Inverta a posição do jogador (isso depende da lógica do seu jogo).
            // Coloque aqui a lógica apropriada para inverter a posição do jogador.
            
            // Agora, mude para a próxima cena.
            GameController.instance.ChangeScene(roomLink);
        }
    }
}
