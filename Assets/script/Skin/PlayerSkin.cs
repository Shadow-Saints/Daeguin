using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public Sprite sprite; // Sprite da nova Skin

    private SpriteRenderer _skin; // SpriteRenderer do player

    private void Start()
    {
        DontDestroyOnLoad(gameObject); // Função responsável por não destruir o player durante a troca de cena
        _skin = GetComponent<SpriteRenderer>(); // Anexando o componente SpriteRenderer
    }

    private void Update()
    {
        _skin.sprite = sprite; // Alterando a skin
    }
}
