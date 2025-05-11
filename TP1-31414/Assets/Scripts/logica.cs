using UnityEngine; // Importa a biblioteca base do Unity
using UnityEngine.SceneManagement; // Biblioteca que permite carregar e mudar de cenas

public class logica : MonoBehaviour
{
    public GameObject PainelInstrucoes; // Refer�ncia ao painel das instru��es
    public GameObject PainelJogo; // Refer�ncia ao painel do menu principal
    public void ComecarJogo() // Fun��o chamada ao clicar no bot�o "Come�ar Jogo"
    {
        SceneManager.LoadScene("Level1"); // Carrega a cena com o nome Level1
    }

    public void MostrarInstrucoes() // Fun��o chamada ao clicar no bot�o "Instru��es"
    {
        PainelInstrucoes.SetActive(true);  // Mostra o painel de instru��es
        PainelJogo.SetActive(false); // Esconde o painel do menu principal
    }

    public void FecharInstrucoes() //Fun�ao chamada ao clicar no bot�o "voltar atr�s"
    {
        PainelInstrucoes.SetActive(false); // fecha o painel
        PainelJogo.SetActive(true);
    }
   
}
