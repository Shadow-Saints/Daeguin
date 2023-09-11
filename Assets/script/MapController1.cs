using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController1 : MonoBehaviour
{
    [SerializeField]
    private int roomLink;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.transform.position = -collision.transform.position;
            GameController.instance.ChangeScene(roomLink);
        }


    }


}
