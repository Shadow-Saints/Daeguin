using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int _Skinindex; // Indice da skin do Player

    public static GameController instance; // Instancia do script GameController

    [SerializeField]
    private SeletorDeSkin[] _skin; // Array responsável por armazenar as skins

    [SerializeField]
    private PlayerSkin _playerskin;

    [SerializeField]
    private float _life;

    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private GameObject _internalCanvas;

    [SerializeField] private TextMeshProUGUI _timerText;

    private float _timerSec;
    private float _timerMins;

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
        ChangeSkin(0);
        _life = 100;
        _slider.value = _life;
    }

    public void ChangeSkin(int increase) // Alterando a skin do Player
    {
        _Skinindex += increase; // incrementando o index da skin
        _playerskin._newSkin = _skin[_Skinindex]; // Trocando a skin do Player      
    }

    public void Damege(int damege) 
    {
        if (_life < 100)
        {
            _life -= damege;
            Debug.Log(_life.ToString());
            _slider.value = _life;
        }else if(_life > 100) 
        {
            _life = 100;
        }
    }

    public void ChangeScene(int lvl) // Trocando de cena
    {
        SceneManager.LoadScene(lvl);
        if (lvl > 1) 
        {
            _internalCanvas.SetActive(true);
        }
        else 
        {
            _internalCanvas.SetActive(false);
        }

    }

    private void Timer() 
    {
        if (_timerSec > 60) 
        {
            _timerMins++;
        }
        _timerSec += Time.deltaTime;

        _timerText.text = _timerMins.ToString("F0") + ":" + _timerSec.ToString("F0");
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex > 1) 
        {
            Timer();
        }else 
        {
            _timerSec = 0;
            _timerMins = 0;
        }
    }

}
