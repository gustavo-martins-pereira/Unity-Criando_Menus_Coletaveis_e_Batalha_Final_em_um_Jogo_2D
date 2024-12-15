using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Classe respons�vel pela l�gica do menu de pausa no jogo.
// Gerencia as intera��es do jogador com os bot�es do menu de pausa, como continuar o jogo,
// acessar o menu de op��es ou retornar ao menu principal.
public class PauseUI : MonoBehaviour
{
    // Refer�ncia ao bot�o "Continuar" no menu de pausa.
    // Permite que uma a��o seja executada quando o jogador clica neste bot�o.
    [SerializeField] private Button continueButton;

    // Refer�ncia ao bot�o "Op��es" no menu de pausa.
    // Utilizado para abrir o menu de op��es quando clicado.
    [SerializeField] private Button optionsButton;

    // Refer�ncia ao bot�o "Sair para o Menu Principal" no menu de pausa.
    // Executa a a��o de retornar ao menu principal do jogo.
    [SerializeField] private Button quitToMenuButton;

    // M�todo chamado automaticamente pela Unity quando o script � inicializado.
    // Configura os eventos de clique (listeners) para cada bot�o do menu de pausa.
    private void Awake()
    {
        // Adiciona um listener ao bot�o "Continuar" que chama o m�todo ClosePauseMenu
        // quando o bot�o � clicado.
        continueButton.onClick.AddListener(ClosePauseMenu);

        // Adiciona um listener ao bot�o "Op��es" que chama o m�todo OpenOptionsMenu
        // quando o bot�o � clicado.
        optionsButton.onClick.AddListener(OpenOptionsMenu);

        // Adiciona um listener ao bot�o "Sair para o Menu Principal" que chama o m�todo GoToMainMenu
        // quando o bot�o � clicado.
        quitToMenuButton.onClick.AddListener(GoToMainMenu);
    }

    // M�todo respons�vel por fechar o menu de pausa.
    // Chamado quando o jogador clica no bot�o "Continuar".
    private void ClosePauseMenu()
    {
        // Desativa o GameObject associado ao menu de pausa,
        // fazendo com que o menu desapare�a da interface.
        gameObject.SetActive(false);
    }

    // M�todo respons�vel por abrir o menu de op��es.
    // Chamado quando o jogador clica no bot�o "Op��es".
    private void OpenOptionsMenu()
    {
        // Acessa o GameManager para abrir o painel de op��es
        // utilizando o m�todo dispon�vel no UIManager.
        GameManager.Instance.UIManager.OpenOptionsPanel();
    }

    // M�todo respons�vel por retornar ao menu principal.
    // Chamado quando o jogador clica no bot�o "Sair para o Menu Principal".
    private void GoToMainMenu()
    {
        // Carrega a cena do menu principal,
        // substituindo a cena atual pela cena chamada "Menu".
        SceneManager.LoadScene("Menu");
    }
}
