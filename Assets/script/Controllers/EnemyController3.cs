using UnityEngine;

public class EnemyController3 : MonoBehaviour
{
    private Rigidbody2D _rig;

    private SpriteRenderer _renderer;
    private Animator _anim;

    void Go()
    {
        float random = Random.Range(0, 2);
        if (random < 1)
        {
            _rig.AddForce(new Vector2(100, -15));
        }
        else
        {
            _rig.AddForce(new Vector2(-200, -15));
        }
    }

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rig = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        Invoke(nameof(Go), 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.CompareTag("Pelicano")|| gameObject.CompareTag("Polvo")) && collision.gameObject.CompareTag("Atack"))
        {
            _anim.SetBool("Atack", true);
            Invoke(nameof(StopAtack), 1);
        }
    }

    private void Update()
    {
        if (_rig.velocity.x == 0)
        {
            _rig.velocity = new Vector2(Random.Range(1, 3), _rig.velocity.y);
        }
        else if (_rig.velocity.y == 0)
        {
            _rig.velocity = new Vector2(_rig.velocity.x, Random.Range(1, 3));
        }

        if (_rig.velocity.x > 0)
        {
            _renderer.flipX = false;
        }
        else if (_rig.velocity.x < 0)
        {
            _renderer.flipX = true;
        }
    }

    private void StopAtack() 
    {
        _anim.SetBool("Atack", false);
    }

}
