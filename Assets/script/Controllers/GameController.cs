using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int _Skinindex;
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

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
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

        // Verifique se a cena atual é a cena do jogo (onde o jogador pode perder vida)
        if (SceneManager.GetActiveScene().buildIndex == 2) // Substitua o número 2 pelo índice da cena do jogo
        {
            // Inicialize a vida e o slider
            _life = 100;
            _slider.value = _life;
        }
    }

    public void ChangeSkin(int increase)
    {
        _Skinindex += increase;
        _playerskin._newSkin = _skin[_Skinindex];
    }

    public void Damege(int damege)
    {
        if (_life < 100)
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
        if (lvl > 1)
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

//criar um cena para ser a de game over e recaregar
        SceneManager.LoadScene(1);
    }
}
