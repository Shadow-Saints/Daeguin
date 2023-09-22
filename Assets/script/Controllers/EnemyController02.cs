using TMPro;
using UnityEngine;

public class EnemyController02 : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Velocidade de movimento do inimigo
    private Transform _target; // Referência ao jogador 
    private Vector3 _direction;

    private SpriteRenderer _render;
    private Animator _anim;

    void Start()
    {
        // Encontre a transformação do jogador
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _render = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
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
            transform.Translate(moveSpeed * Time.deltaTime * _direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.CompareTag("Pedro") || gameObject.CompareTag("NeveQueMorde")) && collision.gameObject.CompareTag("Atack"))
        {
            _anim.SetBool("Atack", true);
            Invoke(nameof(StopAtack), 1);
        }
    }
    private void Animation()
    {
        if (_direction.x < 0)
        {
            _render.flipX = true;
        }
        else if (_direction.x > 0)
        {
            _render.flipX = false;
        }
    }

    private void StopAtack() 
    {
        _anim.SetBool("Atack", false);
    }

}
