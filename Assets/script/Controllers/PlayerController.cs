using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables

    private float _horizontal;
    private float _vertical;

    [Header("Movement")]
    [SerializeField] private float speed = 5f; // Velocidade padrão de movimento
    [SerializeField] private float dashSpeed = 10f; // Velocidade do dash
    [SerializeField] private float _PlayerSpeedAfterDamage = 5f; // Velocidade do jogador após sofrer dano
    [SerializeField] private float runSpeed = 7f; // Velocidade de corrida

    private float currentSpeed; // Velocidade atual do jogador
    private bool isRunning = false; // Verifica se o jogador está correndo

    [Header("Dash")]
    [SerializeField] private float dashCooldown = 1f; // Tempo de recarga do dash
    private bool canDash = true; // Flag para controlar se o jogador pode dar dash
    private bool isDashing = false; // Verifica se o jogador está dando dash

    [Header("Health")]
    [SerializeField] private int maxHealth = 100; // Vida máxima do jogador
    [SerializeField]private int currentHealth; // Vida atual do jogador

    [Header("Components")]
    private Animator _anim; // Componente do Animator
    private SpriteRenderer _renderer; // Componente do SpriteRenderer
    private Rigidbody2D _rig; // Componente do Rigidbody2D
    private TrailRenderer _trail; // Componente do TrailRenderer
    [SerializeField] private GameObject _gun; // Referência ao objeto de arma
    [SerializeField] private GameObject _shield; // Referência ao objeto de escudo
    private bool _noDamage;

    public static PlayerController instance;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        InitializeComponents();
        InitializeHealth();
        _noDamage = false;

    }

    private void Update()
    {
        HandleInput();
        UpdateGunPosition();
    }

    private void FixedUpdate()
    {
        Move();
        HandleDash();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollisionWithEnemy(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleTriggerEnter(collision);
    }

    private void HandleTriggerEnter(Collider2D collision)
{
    if (collision.gameObject.name == "DashStopper")
    {
        StopDash();
    }

    if (collision.CompareTag("Escudo"))
    {
         _noDamage = true;   
        _shield.SetActive(true);
        Destroy(collision.gameObject);
    }

    if (collision.CompareTag("Peixe"))
    {
        GameController.instance.Damege(-15);
        Destroy(collision.gameObject);
        Invencible(2);
    }
}

    #endregion

    #region Initialization

    private void InitializeComponents()
    {
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _rig = GetComponent<Rigidbody2D>();
        _trail = GetComponent<TrailRenderer>();
    }

    private void InitializeHealth()
    {
        currentHealth = maxHealth;
    }

    #endregion

    #region Input Handling

    private void HandleInput()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            DirectionDash();
        }
        HandleRunningInput();
    }

    private void HandleRunningInput()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isRunning ? runSpeed : speed;
    }

    #endregion

    #region Movement

    private void Move()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
            _rig.velocity = new Vector3(_horizontal * currentSpeed, _vertical * currentSpeed, 0);
        
            _gun.SetActive(true);
            _renderer.enabled = true;
            ;
        }else if (SceneManager.GetActiveScene().buildIndex == 1) 
        {
            _gun.SetActive(false);
            _renderer.enabled = true;
        }
        else
        {
            _gun.SetActive(false);
            _renderer.enabled = false;
        }



        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (verticalInput == 1)
        {
            _anim.SetFloat("Dir", 1);
            _renderer.flipX = false;
        }
        else if (verticalInput == -1)
        {
            _anim.SetFloat("Dir", 0);
            _renderer.flipX = false;
        }
        else if (horizontalInput == 1)
        {
            _anim.SetFloat("Dir", 2);
            _renderer.flipX = false;
        }
        else if (horizontalInput == -1)
        {
            _anim.SetFloat("Dir", 2);
            _renderer.flipX = true;
        }
    }

    private void UpdateGunPosition()
    {
        if (_gun.activeSelf)
        {
            _gun.transform.position = transform.position;
        }
    }

    #endregion

    #region Dash

    private void DirectionDash()
    {
        if (Input.GetKey(KeyCode.LeftControl) && canDash)
        {
            Vector2 dashDirection = GetDashDirection();
            if (dashDirection != Vector2.zero)
            {
                StartCoroutine(Dash(dashDirection));
            }
        }

    }

    private Vector2 GetDashDirection()
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

        return dashDirection;
    }

    private IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        isDashing = true;
        _trail.emitting = true;
        _rig.velocity = direction * dashSpeed;

        yield return new WaitForSeconds(dashCooldown);

        StopDash();
        yield return new WaitForSeconds(0.2f);

        canDash = true;
    }

    private void StopDash()
    {
        isDashing = false;
        _rig.velocity = Vector2.zero;
        _trail.emitting = false;
    }

    private void HandleDash()
    {
        if (isDashing)
        {
            _rig.velocity = _rig.velocity.normalized * dashSpeed;
        }
    }

    #endregion

    #region Health and Damage

    private void HandleCollisionWithEnemy(Collision2D collision)
    {
        if (IsEnemyCollision(collision))
        {
            if (!_noDamage)
            {
                int damage = GetDamageForEnemy(collision.gameObject.tag);
                GameController.instance.Damege(damage);
                TakeDamage(damage);
                Destroy(collision.gameObject);
            }
            else 
            {
                Destroy(collision.gameObject);
                _noDamage = false;
            }
        }
    }

    private bool IsEnemyCollision(Collision2D collision)
    {
        return collision.gameObject.CompareTag("Pedro")
            || collision.gameObject.CompareTag("Cachoro")
            || collision.gameObject.CompareTag("NeveQueMorde")
            || collision.gameObject.CompareTag("Ciclista")
            || collision.gameObject.CompareTag("BixoDeNeve");
    }

    private int GetDamageForEnemy(string enemyTag)
    {
        switch (enemyTag)
        {
            case "Pedro":
                return 10;
            case "Cachoro":
                return 15;
            case "NeveQueMorde":
                return 20;
            case "Ciclista":
                return 15;
            case "BixoDeNeve":
                return 5;
            default:
                return 0;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (GameController.instance == null)
        {
            Debug.LogError("Fudeu");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        InitializeComponents();
        InitializeHealth();
        _gun.SetActive(false);
        GameController.instance.Die(); // Chama o método Die do GameController
        currentHealth = maxHealth;
        _noDamage = false;
        transform.position = Vector3.zero;
        _renderer.enabled = false;
        StopDash();
        //Destroy(gameObject); // Destrua o jogador quando a saúde chegar a zero

        
    }

    #endregion

    #region Invincibility

    private IEnumerator DecreasePlayerSpeed(float duration)
    {
        currentSpeed = _PlayerSpeedAfterDamage;
        yield return new WaitForSeconds(duration);
        currentSpeed = isDashing ? dashSpeed : speed;
    }

    public void Invencible(float seconds)
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

    #endregion
}