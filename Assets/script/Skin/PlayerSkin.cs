using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public SeletorDeSkin _newSkin;

    private SpriteRenderer _skin; // SpriteRenderer do player

    private Animator _anim;
    private void Start()
    {
        DontDestroyOnLoad(gameObject); // Função responsável por não destruir o player durante a troca de cena
        _skin = GetComponent<SpriteRenderer>(); // Anexando o componente SpriteRenderer
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _skin.sprite = _newSkin.sprite; // Alterando a skin
        _anim.runtimeAnimatorController = _newSkin.animator;
    }
}
