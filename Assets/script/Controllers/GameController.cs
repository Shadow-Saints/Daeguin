using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _Skinindex;
    public static GameController instance;
    [SerializeField]
    private SeletorDeSkin[] _skin;
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

    private float _recordTimer;
    private float _totalTimer;


    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
    }

    private void Start()
    {
        _Skinindex = 0;
        ChangeSkin(0);
        _life = 100;
        _slider.value = _life;
        _recordTimer = PlayerPrefs.GetFloat("Record");

        // Verifique se a cena atual é a cena do jogo (onde o jogador pode perder vida)
        
    }

    public void ChangeSkin(int increase)
    {
        _Skinindex += increase;
        _playerskin._newSkin = _skin[_Skinindex];
    }

    public void Damege(int damege)
    {
        if (_life <= 100)
        {
            _life -= damege;
            Debug.Log(_life.ToString());
            _slider.value = _life;
        }
        else if (_life > 100)
        {
            _life = 100;
        }
    }

    public void ChangeScene(int lvl)
    {
        SceneManager.LoadScene(lvl);
        if (lvl == 2)
        {
            _internalCanvas.SetActive(true);
        }
        else
        {
            _internalCanvas.SetActive(false);
        }
    }

    public void Die()
    {
        // Redefina a vida e o slider
        _life = 100;
        _slider.value = _life;
        _Skinindex = 0;
        ChangeSkin(_Skinindex);

        ChangeScene(3);
    }

    private void Timer()
    {
        if (_timerSec > 60)
        {
            _timerMins++;
            _timerSec = 0;
        }
            _timerSec += Time.deltaTime;
            _totalTimer += Time.deltaTime;

            _timerText.text = _timerMins.ToString("F0") + ":" + _timerSec.ToString("F0");

        if (_totalTimer > _recordTimer)
        {
            _recordTimer = _totalTimer;
            PlayerPrefs.SetString("Timer", _timerText.text);
            PlayerPrefs.SetFloat("Record", _recordTimer);
        }

        

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Timer();
        }
        else
        {
            _timerSec = 0;
            _timerMins = 0;
        }
    }
}
