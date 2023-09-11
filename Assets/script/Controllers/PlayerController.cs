using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer _renderer;
    private float _horizontal;
    private float _vertical;

    [SerializeField] private float _Speed;
    [SerializeField] private float _DashSpeed;
    [SerializeField] private float _dashingCooldown;
    [SerializeField] private float _RunSpeed;
    private float _currentSpeed;

    private bool canDash = true;
    private bool isDashing = false;
    private bool isRunning = false;

    [SerializeField] private Rigidbody2D _rig;
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private float _PlayerSpeedAfterDamage = 5f;

    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _shield;

    public event System.Action<int> OnEnemyCollision; // Evento para comunicar colisões com inimigos

    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        DirectionDash();

        if (isDashing)
        {
            return;
        }

        _currentSpeed = isDashing ? _DashSpeed : _Speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = _RunSpeed;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void FixedUpdate()
    {
        Move();

        if (isDashing)
        {
            _rig.velocity = _rig.velocity.normalized * _DashSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pedro"))
        {
            GameController.instance.Damege(5);
            Invencible(1); // Chame a função Invencible do PlayerController
        }
        else if (collision.gameObject.CompareTag("Cachoro"))
        {
            GameController.instance.Damege(15);
            Invencible(1); // Chame a função Invencible do PlayerController
        }
        else if (collision.gameObject.CompareTag("Fotografo"))
        {
            GameController.instance.Damege(10);
            Invencible(1); // Chame a função Invencible do PlayerController
        }
        else if (collision.gameObject.CompareTag("Ciclista"))
        {
            GameController.instance.Damege(20);
            Invencible(1); // Chame a função Invencible do PlayerController
        }
        else if (collision.gameObject.CompareTag("BixoDeNeve"))
        {
            GameController.instance.Damege(12);
            Invencible(1); // Chame a função Invencible do PlayerController
            StartCoroutine(DecreasePlayerSpeed(10f)); // Diminui a velocidade por 10 segundos.
        }
        else if (collision.gameObject.CompareTag("NeveQueMorde"))
        {
            GameController.instance.Damege(17);
            Invencible(1); // Chame a função Invencible do PlayerController
            StartCoroutine(DecreasePlayerSpeed(10f)); // Diminui a velocidade por 10 segundos.
        }
        else if (collision.gameObject.CompareTag("Urso"))
        {
            GameController.instance.Damege(25);
            Invencible(1); // Chame a função Invencible do PlayerController
        }
        else if (collision.gameObject.CompareTag("BolaDeNeveUrso"))
        {
            GameController.instance.Damege(15);
            Invencible(1); // Chame a função Invencible do PlayerController
            StartCoroutine(DecreasePlayerSpeed(10f)); // Diminui a velocidade por 10 segundos.
        }
    }

    private IEnumerator DecreasePlayerSpeed(float duration)
    {
        _currentSpeed = _PlayerSpeedAfterDamage; // Define a velocidade do jogador após sofrer dano.
        yield return new WaitForSeconds(duration);
        _currentSpeed = isDashing ? _DashSpeed : _Speed; // Retorna à velocidade normal após o tempo especificado.
    }

    public void Invencible(float seconds) // Modificamos para ser pública
    {
        CapsuleCollider2D collider2D = GetComponent<CapsuleCollider2D>();
        collider2D.enabled = false;
        Invoke(nameof(Tangible), seconds);
    }

    private void Tangible()
    {
        CapsuleCollider2D collider2D = GetComponent<CapsuleCollider2D>();
        collider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DashStopper"))
        {
            StopDash();
        }

        if (collision.CompareTag("Escudo"))
        {
            _shield.SetActive(true);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Peixe"))
        {
            GameController.instance.Damege(-15);
            Destroy(collision.gameObject);
            Invencible(2);
        }
    }

    private void StopDash()
    {
        isDashing = false;
        _rig.velocity = Vector2.zero;
        _trail.emitting = false;
    }

    private IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        isDashing = true;
        _trail.emitting = true;
        _rig.velocity = direction * _DashSpeed;
        yield return new WaitForSeconds(_dashingCooldown);
        StopDash();
        yield return new WaitForSeconds(0.2f);
        canDash = true;
    }

    private void DirectionDash()
    {
        if (Input.GetKey(KeyCode.LeftControl) && canDash)
        {
            Vector2 dashDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
            {
                dashDirection = Vector2.up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dashDirection = Vector2.down;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dashDirection = Vector2.right;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                dashDirection = Vector2.left;
            }

            if (dashDirection != Vector2.zero)
            {
                StartCoroutine(Dash(dashDirection));
            }
        }
    }

    private void Move()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
            _rig.velocity = new Vector3(_horizontal * _currentSpeed, _vertical * _currentSpeed, 0);
            if (_gun.activeSelf == false)
            {
                _gun.SetActive(true);
            }
        }
        else
        {
            _gun.SetActive(false);
        }

        if (Input.GetAxisRaw("Vertical") == 1)
        {
            _anim.SetFloat("Dir", 1);
            _renderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            _anim.SetFloat("Dir", 0);
            _renderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            _anim.SetFloat("Dir", 2);
            _renderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            _anim.SetFloat("Dir", 2);
            _renderer.flipX = true;
        }
    }
}