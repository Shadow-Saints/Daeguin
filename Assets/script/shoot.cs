using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    [SerializeField]
    private float _speed; // Velocidade do tiro
    

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime); // Movimento do tiro
    }
}
