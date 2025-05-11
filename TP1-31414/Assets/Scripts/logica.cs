using UnityEngine; // Importa a biblioteca base do Unity
using UnityEngine.SceneManagement; // Biblioteca que permite carregar e mudar de cenas

public class logica : MonoBehaviour
{
    public GameObject PainelInstrucoes; // Referência ao painel das instruções
    public GameObject PainelJogo; // Referência ao painel do menu principal
    public void ComecarJogo() // Função chamada ao clicar no botão "Começar Jogo"
    {
        SceneManager.LoadScene("Level1"); // Carrega a cena com o nome Level1
    }

    public void MostrarInstrucoes() // Função chamada ao clicar no botão "Instruções"
    {
        PainelInstrucoes.SetActive(true);  // Mostra o painel de instruções
        PainelJogo.SetActive(false); // Esconde o painel do menu principal
    }

    public void FecharInstrucoes() //Funçao chamada ao clicar no botão "voltar atrás"
    {
        PainelInstrucoes.SetActive(false); // fecha o painel
        PainelJogo.SetActive(true);
    }
   
}
