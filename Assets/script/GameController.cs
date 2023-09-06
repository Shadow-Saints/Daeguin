using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int _Skinindex; // Indice da skin do Player

    public static GameController instance; // Instancia do script GameController

    [SerializeField]
    private SeletorDeSkin[] _skin; // Array responsável por armazenar as skins

    [SerializeField]
    private PlayerSkin _playerskin;

    private void Awake()
    {
        DontDestroyOnLoad(this); // Função responsável por não destruir o player durante a troca de cena

        // Implementando o Padrão singleton
        if (instance == null) 
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _Skinindex = 0;
    }

    public void ChangeSkin(int increase) // Alterando a skin do Player
    {
        _Skinindex += increase; // incrementando o index da skin
        _playerskin._newSkin = _skin[_Skinindex]; // Trocando a skin do Player      
    }

    public void changeScene(int lvl) // Trocando de cena
    {
        SceneManager.LoadScene(lvl);
    }
    
}
