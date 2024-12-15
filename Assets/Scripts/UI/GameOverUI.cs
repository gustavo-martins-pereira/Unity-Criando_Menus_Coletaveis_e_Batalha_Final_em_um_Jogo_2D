using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Classe respons�vel por gerenciar a interface de usu�rio na tela de Game Over.
// Permite ao jogador reiniciar o jogo ou voltar ao menu principal.
public class GameOverUI : MonoBehaviour
{
    // Refer�ncia ao bot�o de reiniciar o jogo. Esse bot�o deve ser configurado no inspetor da Unity.
    [SerializeField] private Button restartButton;

    // Refer�ncia ao bot�o de voltar ao menu principal. Esse bot�o deve ser configurado no inspetor da Unity.
    [SerializeField] private Button menuButton;

    // M�todo chamado automaticamente pela Unity durante a inicializa��o do objeto.
    // Configura os eventos de clique para os bot�es de reiniciar e de menu.
    private void Awake()
    {
        // Adiciona um listener ao bot�o de reiniciar, associando-o ao m�todo que reinicia o jogo.
        restartButton.onClick.AddListener(RestartGame);

        // Adiciona um listener ao bot�o de menu, associando-o ao m�todo que carrega a cena do menu principal.
        menuButton.onClick.AddListener(GoToMenuGame);
    }

    // M�todo respons�vel por reiniciar o jogo.
    // Carrega a cena chamada "Gameplay", que deve estar registrada no Build Settings.
    private void RestartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    // M�todo respons�vel por voltar ao menu principal.
    // Carrega a cena chamada "Menu", que deve estar registrada no Build Settings.
    private void GoToMenuGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
