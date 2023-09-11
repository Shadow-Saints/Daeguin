using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string _nomeDoLevelDeJogo;
    [SerializeField] private GameObject _painelMenuInicial;
    [SerializeField] private GameObject _painelOpcoes;
    [SerializeField] private GameObject _painelControles;
    [SerializeField] private GameObject _painelCreditos;

    public void Jogar()
    {
        SceneManager.LoadScene(_nomeDoLevelDeJogo);
    }

    public void AbrirOpcoes()
    {
        _painelMenuInicial.SetActive(false);
        _painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        _painelOpcoes.SetActive(false);
        _painelMenuInicial.SetActive(true); // Adicionado para reativar o painel do menu inicial ao fechar as opções.
    }

    public void AbrirControles()
    {
        _painelMenuInicial.SetActive(false);
        _painelControles.SetActive(true);
    }

    public void FecharControles()
    {
        _painelControles.SetActive(false);
        _painelMenuInicial.SetActive(true); // Adicionado para reativar o painel do menu inicial ao fechar os controles.
    }

    public void AbrirCreditos()
    {
        _painelMenuInicial.SetActive(false);
        _painelCreditos.SetActive(true);
    }

    public void FecharCreditos()
    {
        _painelCreditos.SetActive(false);
        _painelMenuInicial.SetActive(true); // Adicionado para reativar o painel do menu inicial ao fechar os créditos.
    }

    public void Sair()
    {
        Debug.Log("Saiu");
        Application.Quit();
    }
}
