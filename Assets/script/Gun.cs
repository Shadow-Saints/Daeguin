using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera; // Camera

    [SerializeField]
    private Vector2 _mousePosition; // Posição do Mouse

    [SerializeField]
    private GameObject _shoot; // Objeto do tiro

    [SerializeField]
    private float _shootDelay; // Delay do tiro
    private float _timer;

    [SerializeField]
    private Rigidbody2D _rig; // Componente Rigidbody2D

    private float _mouseAngle; // Angulo de posição do mouse em relação ao Player


    private void OnEnable()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Rotate(); // Chamando a rotação da mira 
        Shoot(); // Chamando a função de atirar 
    }

    void Shoot() // Disparo
    {
        if (Input.GetMouseButtonDown(0) && _timer > _shootDelay) 
        {
            GameObject newShoot = Instantiate(_shoot); // Instanciando o projétil
            newShoot.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0,0, _mouseAngle)); // posicionando o projétil
            _timer = 0;
        }
        _timer += Time.deltaTime;
    }

    private void Rotate() // Posição da mira
    {
        // Seguir Mouse
        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 _viewDirection = _mousePosition - _rig.position;
        _mouseAngle = Mathf.Atan2(_viewDirection.y, _viewDirection.x) * Mathf.Rad2Deg;
    }

}
