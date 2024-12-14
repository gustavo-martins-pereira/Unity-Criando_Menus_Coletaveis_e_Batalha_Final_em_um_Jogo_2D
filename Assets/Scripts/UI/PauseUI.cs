using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Classe responsável pela lógica do menu de pausa no jogo.
// Essa classe gerencia a interação dos botões de pausa, como continuar o jogo,
// abrir o menu de opções ou retornar ao menu principal.
public class PauseUI : MonoBehaviour
{
    // Referências aos botões da interface do menu de pausa.
    // Essas referências são configuradas no Inspetor do Unity (usando [SerializeField]).
    [SerializeField] private Button continueButton;     // Botão para continuar o jogo.
    [SerializeField] private Button optionsButton;      // Botão para abrir o menu de opções.
    [SerializeField] private Button quitToMenuButton;   // Botão para retornar ao menu principal.

    // Método chamado pela Unity quando o script é inicializado.
    // Configura os listeners para os botões, associando ações específicas a cada um.
    private void Awake()
    {
        // Associa o método `ClosePauseMenu` ao evento de clique do botão "Continuar".
        continueButton.onClick.AddListener(ClosePauseMenu);

        // Associa o método `OpenOptionsMenu` ao evento de clique do botão "Opções".
        optionsButton.onClick.AddListener(OpenOptionsMenu);

        // Associa o método `GoToMainMenu` ao evento de clique do botão "Sair para o Menu Principal".
        quitToMenuButton.onClick.AddListener(GoToMainMenu);
    }

    // Método chamado quando o jogador clica no botão "Continuar".
    // Fecha o menu de pausa, desativando o GameObject associado.
    private void ClosePauseMenu()
    {
        gameObject.SetActive(false); // Desativa o menu de pausa na interface.
    }

    // Método chamado quando o jogador clica no botão "Opções".
    // Abre o menu de opções, solicitando ao `UIManager` que exiba o painel de opções.
    private void OpenOptionsMenu()
    {
        print("Opening options"); // Mensagem de depuração exibida no Console.
        GameManager.Instance.UIManager.OpenOptionsPanel(); // Chama o método responsável por abrir o painel de opções.
    }

    // Método chamado quando o jogador clica no botão "Sair para o Menu Principal".
    // Carrega a cena principal do jogo (menu principal).
    private void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu"); // Transiciona para a cena chamada "Menu".
    }
}
