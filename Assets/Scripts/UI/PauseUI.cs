using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Classe responsável pela lógica do menu de pausa no jogo.
// Gerencia as interações do jogador com os botões do menu de pausa, como continuar o jogo,
// acessar o menu de opções ou retornar ao menu principal.
public class PauseUI : MonoBehaviour
{
    // Referência ao botão "Continuar" no menu de pausa.
    // Permite que uma ação seja executada quando o jogador clica neste botão.
    [SerializeField] private Button continueButton;

    // Referência ao botão "Opções" no menu de pausa.
    // Utilizado para abrir o menu de opções quando clicado.
    [SerializeField] private Button optionsButton;

    // Referência ao botão "Sair para o Menu Principal" no menu de pausa.
    // Executa a ação de retornar ao menu principal do jogo.
    [SerializeField] private Button quitToMenuButton;

    // Método chamado automaticamente pela Unity quando o script é inicializado.
    // Configura os eventos de clique (listeners) para cada botão do menu de pausa.
    private void Awake()
    {
        // Adiciona um listener ao botão "Continuar" que chama o método ClosePauseMenu
        // quando o botão é clicado.
        continueButton.onClick.AddListener(ClosePauseMenu);

        // Adiciona um listener ao botão "Opções" que chama o método OpenOptionsMenu
        // quando o botão é clicado.
        optionsButton.onClick.AddListener(OpenOptionsMenu);

        // Adiciona um listener ao botão "Sair para o Menu Principal" que chama o método GoToMainMenu
        // quando o botão é clicado.
        quitToMenuButton.onClick.AddListener(GoToMainMenu);
    }

    // Método responsável por fechar o menu de pausa.
    // Chamado quando o jogador clica no botão "Continuar".
    private void ClosePauseMenu()
    {
        // Desativa o GameObject associado ao menu de pausa,
        // fazendo com que o menu desapareça da interface.
        gameObject.SetActive(false);
    }

    // Método responsável por abrir o menu de opções.
    // Chamado quando o jogador clica no botão "Opções".
    private void OpenOptionsMenu()
    {
        // Acessa o GameManager para abrir o painel de opções
        // utilizando o método disponível no UIManager.
        GameManager.Instance.UIManager.OpenOptionsPanel();
    }

    // Método responsável por retornar ao menu principal.
    // Chamado quando o jogador clica no botão "Sair para o Menu Principal".
    private void GoToMainMenu()
    {
        // Carrega a cena do menu principal,
        // substituindo a cena atual pela cena chamada "Menu".
        SceneManager.LoadScene("Menu");
    }
}
