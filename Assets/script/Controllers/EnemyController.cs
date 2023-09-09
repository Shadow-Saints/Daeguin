using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Velocidade de movimento do inimigo

    private Rigidbody2D _rig;
    private Vector2 _randomDirection;
    private Camera _mainCamera;
    private float _minX, _maxX, _minY, _maxY;

    void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;

        // Calcula os limites da tela com base na posição da câmera
        //CalculateScreenBounds();

        // Inicie o movimento aleatório
        GenerateRandomDirection();
    }

    void Update()
    {
        // Move o inimigo na direção aleatória
        _rig.velocity = _randomDirection * moveSpeed;

        
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifique se o inimigo colidiu com um obstáculo
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Pare o movimento aleatório e gere uma nova direção
            GenerateRandomDirection();
        }
    }

    /* private void CalculateScreenBounds()
     {
         // Obtém a posição da câmera em relação ao mundo
         Vector3 cameraPosition = _mainCamera.transform.position;

         // Calcula os limites da tela usando a câmera e o tamanho da tela
         _minX = cameraPosition.x - _mainCamera.orthographicSize * _mainCamera.aspect;
         _maxX = cameraPosition.x + _mainCamera.orthographicSize * _mainCamera.aspect;
         _minY = cameraPosition.y - _mainCamera.orthographicSize;
         _maxY = cameraPosition.y + _mainCamera.orthographicSize;
     }/*/

    private void GenerateRandomDirection()
    {
        // Gera uma nova direção aleatória
        _randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}