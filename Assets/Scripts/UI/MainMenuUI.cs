using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Classe responsável pelo gerenciamento do menu principal da interface do jogo.
// Essa classe controla os botões do menu principal, permitindo iniciar o jogo, abrir o menu de opções e sair do jogo.
public class MainMenuUI : MonoBehaviour
{
    // Referência ao painel de opções, que será exibido ou ocultado quando necessário.
    [SerializeField] private GameObject optionsPanel;

    // Agrupamento de propriedades da interface do menu principal.
    [Header("Menu UI properties")]
    [SerializeField] private Button startButton;    // Botão para iniciar o jogo.
    [SerializeField] private Button optionsButton;  // Botão para abrir o menu de opções.
    [SerializeField] private Button exitButton;     // Botão para sair do jogo.

    // Método chamado automaticamente quando o objeto é ativado.
    private void OnEnable()
    {
        // Certifica-se de que o painel de opções esteja desativado no início.
        optionsPanel.SetActive(false);

        // Adiciona ouvintes (listeners) aos botões para chamar os métodos apropriados ao serem clicados.
        startButton.onClick.AddListener(GoToGameplayScene);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        exitButton.onClick.AddListener(ExitGame);
    }

    // Método responsável por carregar a cena de gameplay.
    private void GoToGameplayScene()
    {
        // Reproduz o efeito sonoro de clique no botão.
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

        // Carrega a cena chamada "Gameplay".
        SceneManager.LoadScene("Gameplay");
    }

    // Método responsável por abrir o menu de opções.
    private void OpenOptionsMenu()
    {
        // Reproduz o efeito sonoro de clique no botão.
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

        // Exibe o painel de opções.
        optionsPanel.SetActive(true);
    }

    // Método responsável por encerrar o jogo.
    private void ExitGame()
    {
        // Reproduz o efeito sonoro de clique no botão.
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

        // Verifica se o jogo está sendo executado no Editor da Unity.
#if UNITY_EDITOR
        // Se estiver no Editor, encerra o modo de execução.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Se estiver em um build do jogo, fecha o aplicativo.
        Application.Quit();
#endif
    }
}
