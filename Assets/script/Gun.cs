using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera; // Camera

    [SerializeField]
    private Vector2 _mousePosition; // Posição do Mouse

    [SerializeField]
    private GameObject _shoot; // Objeto do tiro

    private Rigidbody2D _rig; // Componente Rigidbody2D

    private void Start()
    {
        _rig = GetComponent<Rigidbody2D>(); // Obtendo o componente Rigidbody2D
    }

    private void Update()
    {
        Rotate(); // Chamando a rotação da mira 
        Shoot(); // Chamando a função de atirar 
    }

    void Shoot() // Disparo
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            GameObject newShoot = Instantiate(_shoot); // Instanciando o projétil
            newShoot.transform.position = transform.position; // Alterando a posição do projétil
            newShoot.transform.rotation = transform.rotation; // Alterando a rotação do projétil
        }
    }

    private void Rotate() // Posição da mira
    {
        // Seguir Mouse
        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 _viewDirection = _mousePosition - _rig.position;
        float _viewAngle = Mathf.Atan2(_viewDirection.y, _viewDirection.x) * Mathf.Rad2Deg;
        _rig.rotation = _viewAngle;
    }

}
