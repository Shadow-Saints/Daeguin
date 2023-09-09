using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController02 : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Velocidade de movimento do inimigo
    private Transform _target; // Referência ao jogador 
    private Vector3 _direction;

    private SpriteRenderer _render;

    void Start()
    {
        // Encontre a transformação do jogador
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Animation();

        if (_target != null)
        {
            // Calcula a direção do jogador
            _direction = _target.position - transform.position;
            _direction.Normalize(); // Normaliza para manter a velocidade constante

            // Move o inimigo na direção do jogador
            transform.Translate(_direction * moveSpeed * Time.deltaTime);
        }
    }

    private void Animation() 
    {
        if (_direction.x < 0) 
        {
            _render.flipX = true;
        }else if (_direction.x > 0) 
        {
            _render.flipX = false;
        }
    }
}
