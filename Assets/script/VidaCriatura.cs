using UnityEngine;

public class VidaCriatura : MonoBehaviour
{
    [SerializeField] private int vidaMaxima = 100; // Vida máxima da criatura
    private int vidaAtual; // Vida atual da criatura

    private void Start()
    {
        vidaAtual = vidaMaxima; // Inicialize a vida atual com a vida máxima no início
    }

    // Função para causar dano à criatura
    public void CausarDano(int quantidadeDano)
    {
        // Reduz a vida atual pela quantidade de dano recebida
        vidaAtual -= quantidadeDano;

        // Verifica se a vida atual chegou a zero ou menos
        if (vidaAtual <= 0)
        {
            Morrer(); // Chama a função de morte quando a vida atinge zero
        }
    }

    // Função para a criatura morrer
    private void Morrer()
    {
        // Ação a ser executada quando a criatura morrer
        Destroy(gameObject); // Por padrão, destruímos o objeto, mas você pode fazer outras coisas aqui
    }

    // Função para recuperar vida da criatura
    public void RecuperarVida(int quantidadeRecuperada)
    {
        // Aumenta a vida atual pela quantidade recuperada
        vidaAtual += quantidadeRecuperada;

        // Certifique-se de que a vida atual não exceda a vida máxima
        vidaAtual = Mathf.Min(vidaAtual, vidaMaxima);
    }
}
