using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Classe respons�vel pela l�gica do menu de pausa no jogo.
// Essa classe gerencia a intera��o dos bot�es de pausa, como continuar o jogo,
// abrir o menu de op��es ou retornar ao menu principal.
public class PauseUI : MonoBehaviour
{
    // Refer�ncias aos bot�es da interface do menu de pausa.
    // Essas refer�ncias s�o configuradas no Inspetor do Unity (usando [SerializeField]).
    [SerializeField] private Button continueButton;     // Bot�o para continuar o jogo.
    [SerializeField] private Button optionsButton;      // Bot�o para abrir o menu de op��es.
    [SerializeField] private Button quitToMenuButton;   // Bot�o para retornar ao menu principal.

    // M�todo chamado pela Unity quando o script � inicializado.
    // Configura os listeners para os bot�es, associando a��es espec�ficas a cada um.
    private void Awake()
    {
        // Associa o m�todo `ClosePauseMenu` ao evento de clique do bot�o "Continuar".
        continueButton.onClick.AddListener(ClosePauseMenu);

        // Associa o m�todo `OpenOptionsMenu` ao evento de clique do bot�o "Op��es".
        optionsButton.onClick.AddListener(OpenOptionsMenu);

        // Associa o m�todo `GoToMainMenu` ao evento de clique do bot�o "Sair para o Menu Principal".
        quitToMenuButton.onClick.AddListener(GoToMainMenu);
    }

    // M�todo chamado quando o jogador clica no bot�o "Continuar".
    // Fecha o menu de pausa, desativando o GameObject associado.
    private void ClosePauseMenu()
    {
        gameObject.SetActive(false); // Desativa o menu de pausa na interface.
    }

    // M�todo chamado quando o jogador clica no bot�o "Op��es".
    // Abre o menu de op��es, solicitando ao `UIManager` que exiba o painel de op��es.
    private void OpenOptionsMenu()
    {
        print("Opening options"); // Mensagem de depura��o exibida no Console.
        GameManager.Instance.UIManager.OpenOptionsPanel(); // Chama o m�todo respons�vel por abrir o painel de op��es.
    }

    // M�todo chamado quando o jogador clica no bot�o "Sair para o Menu Principal".
    // Carrega a cena principal do jogo (menu principal).
    private void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu"); // Transiciona para a cena chamada "Menu".
    }
}
