using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rig; // Componente Rigidbody2D

    [SerializeField]
    private int _speed; // Velocidade

    private void Start()
    { 
        _rig = GetComponent<Rigidbody2D>(); // Anexando o componente Rigidbody2D
    }
    private void FixedUpdate()
    {
        Move(); // Movimento
    }

    private void Move() // Movimento
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) // Se a cena for diferente de zero(seletor de skin): 
        {
            _rig.velocity = new Vector3(Input.GetAxis("Horizontal") * _speed, Input.GetAxis("Vertical") * _speed, 0); // Movimentando o Player
        }

    }
}
