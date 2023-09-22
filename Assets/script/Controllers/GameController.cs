using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Variables
    public static GameController instance;

    [Header("Skin")]
    [SerializeField] private int _Skinindex;
    [SerializeField]
    private SeletorDeSkin[] _skin;
    [SerializeField]
    private PlayerSkin _playerskin;
    [SerializeField]

    [Header("Life")]
    private float _life;
    [Header("Internal Canvas")]
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private GameObject _internalCanvas;
    [SerializeField] private TextMeshProUGUI _timerText;

    [Header("Timer")]
    private float _timerSec;

    private bool _lvl2;

    [SerializeField] private float _timerMins;

    private float _recordTimer;
    private float _totalTimer;

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

    private void Start()
    {
        _Skinindex = 0;
        ChangeSkin(0);
        _life = 100;
        _slider.value = _life;
        _recordTimer = PlayerPrefs.GetFloat("Record");
        
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3)
        {
            Timer();
        }
        else
        {
            _timerSec = 0;
            _timerMins = 0;
        }

        if(_timerMins >= 15 && !_lvl2)
        {
            ChangeScene(3);
            _lvl2 = true;
        }

    }

    #endregion

    #region Skins

    public void ChangeSkin(int increase)
    {

        _Skinindex += increase;
        if (_Skinindex > 3) 
        {
            _Skinindex = 0;
        }else if(_Skinindex < 0) 
        {
            _Skinindex = 3;
        }
        _playerskin._newSkin = _skin[_Skinindex];
    }

    #endregion

    #region Life and Damege
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
    public void Die()
    {
        // Redefina a vida e o slider
        _Skinindex = 0;
        ChangeSkin(0);
        _life = 100;
        _slider.value = _life;
        _recordTimer = PlayerPrefs.GetFloat("Record");
        _internalCanvas.SetActive(false);
        ChangeScene(4);


    }

    #endregion

    #region Scene Manegement
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

    #endregion

    #region Timer

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

    #endregion

}
