using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator _anim; // Componente Animator

    private SpriteRenderer _renderer; // Componente SpriteRenderer

    private float horizontal;
    private float vertical;

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

    [SerializeField] private GameObject _gun;

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
            GameController.instance.Damege(10);
            Invencible(1);
        }
        else if (collision.gameObject.CompareTag("Cachoro"))
        {
            GameController.instance.Damege(20);
            Invencible(1);
        }
    }

    private void Invencible(float seconds)
    {
        CapsuleCollider2D collider2D = GetComponent<CapsuleCollider2D>();
        collider2D.enabled = false;
        Invoke("Tangible", seconds);
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
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            _rig.velocity = new Vector3(horizontal * _currentSpeed, vertical * _currentSpeed, 0);
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