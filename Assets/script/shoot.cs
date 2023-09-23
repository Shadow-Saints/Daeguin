using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class shoot : MonoBehaviour
{
    [SerializeField] private float _speed; // Velocidade da bala
    [SerializeField] private List<string> validEnemyTags = new List<string>(); // Lista de tags de inimigos válidos
    [SerializeField] private int damage; // Dano da bala

    
    [SerializeField]private SpriteRenderer _renderer;

    bool _fliped;

    private void Update()
    {
        // Move a bala
        transform.Translate(Vector2.right * _speed * Time.deltaTime);

        if (transform.rotation.eulerAngles.z > 90)
        {
            _renderer.flipY = true;
            _fliped = true;
        }
        else
        {
            _renderer.flipY = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique se a tag do objeto colidido está na lista de tags de inimigos válidos
        if (validEnemyTags.Contains(other.tag))
        {
            // A tag do objeto colidido está na lista, então causa dano ao inimigo (se aplicável)
            DanoInimigo enemy = other.GetComponent<DanoInimigo>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }else if(other.gameObject.layer == 7) 
        {
            Destroy(this.gameObject); 
        }

        // Destrua a bala após causar dano ou colidir com qualquer objeto
        
    }
}