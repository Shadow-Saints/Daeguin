using System.Collections;
using System.Collections.Generic;
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
    
    private bool canDash = true;
    private bool isDashing = false;
    private bool isRunning = false;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private TrailRenderer _tr;

    private void Update()
    {
        Move();
        DirectionDash();
        
        if (isDashing)
        {
            return;
        }
        
        float currentSpeed = isDashing ? _DashSpeed : _Speed;
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = _RunSpeed;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        
        _rb.velocity = new Vector2(horizontal * currentSpeed, vertical * currentSpeed);
        _anim = GetComponent<Animator>(); // Anexando o componente Animator
        _renderer = GetComponent<SpriteRenderer>(); // Anexando o componente SpriteRenderer
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            _rb.velocity = _rb.velocity.normalized * _DashSpeed;
        }
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
        _rb.velocity = Vector2.zero;
        _tr.emitting = false;
    }

    private IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        isDashing = true;
        _tr.emitting = true;
        _rb.velocity = direction * _DashSpeed;
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
            
        if (Input.GetAxisRaw("Vertical") == 1) 
        {
            _anim.SetFloat("Dir", 1);
            _renderer.flipX = false;
        }
        else if(Input.GetAxisRaw("Vertical") == -1) 
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
            _rb.velocity = new Vector3(horizontal * _Speed, vertical * _Speed, 0);
        }
    }
}
