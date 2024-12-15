using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Classe responsável por gerenciar a interface de usuário na tela de Game Over.
// Permite ao jogador reiniciar o jogo ou voltar ao menu principal.
public class GameOverUI : MonoBehaviour
{
    // Referência ao botão de reiniciar o jogo. Esse botão deve ser configurado no inspetor da Unity.
    [SerializeField] private Button restartButton;

    // Referência ao botão de voltar ao menu principal. Esse botão deve ser configurado no inspetor da Unity.
    [SerializeField] private Button menuButton;

    // Método chamado automaticamente pela Unity durante a inicialização do objeto.
    // Configura os eventos de clique para os botões de reiniciar e de menu.
    private void Awake()
    {
        // Adiciona um listener ao botão de reiniciar, associando-o ao método que reinicia o jogo.
        restartButton.onClick.AddListener(RestartGame);

        // Adiciona um listener ao botão de menu, associando-o ao método que carrega a cena do menu principal.
        menuButton.onClick.AddListener(GoToMenuGame);
    }

    // Método responsável por reiniciar o jogo.
    // Carrega a cena chamada "Gameplay", que deve estar registrada no Build Settings.
    private void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    // Método responsável por voltar ao menu principal.
    // Carrega a cena chamada "Menu", que deve estar registrada no Build Settings.
    private void GoToMenuGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
