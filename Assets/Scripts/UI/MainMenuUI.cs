using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Classe respons�vel pelo gerenciamento do menu principal da interface do jogo.
// Essa classe controla os bot�es do menu principal, permitindo iniciar o jogo, abrir o menu de op��es e sair do jogo.
public class MainMenuUI : MonoBehaviour
{
    // Refer�ncia ao painel de op��es, que ser� exibido ou ocultado quando necess�rio.
    [SerializeField] private GameObject optionsPanel;

    // Agrupamento de propriedades da interface do menu principal.
    [Header("Menu UI properties")]
    [SerializeField] private Button startButton;    // Bot�o para iniciar o jogo.
    [SerializeField] private Button optionsButton;  // Bot�o para abrir o menu de op��es.
    [SerializeField] private Button exitButton;     // Bot�o para sair do jogo.

    // M�todo chamado automaticamente quando o objeto � ativado.
    private void OnEnable()
    {
        // Certifica-se de que o painel de op��es esteja desativado no in�cio.
        optionsPanel.SetActive(false);

        // Adiciona ouvintes (listeners) aos bot�es para chamar os m�todos apropriados ao serem clicados.
        startButton.onClick.AddListener(GoToGameplayScene);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        exitButton.onClick.AddListener(ExitGame);
    }

    // M�todo respons�vel por carregar a cena de gameplay.
    private void GoToGameplayScene()
    {
        // Reproduz o efeito sonoro de clique no bot�o.
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

        // Carrega a cena chamada "Gameplay".
        SceneManager.LoadScene("Gameplay");
    }

    // M�todo respons�vel por abrir o menu de op��es.
    private void OpenOptionsMenu()
    {
        // Reproduz o efeito sonoro de clique no bot�o.
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

        // Exibe o painel de op��es.
        optionsPanel.SetActive(true);
    }

    // M�todo respons�vel por encerrar o jogo.
    private void ExitGame()
    {
        // Reproduz o efeito sonoro de clique no bot�o.
        GameManager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

        // Verifica se o jogo est� sendo executado no Editor da Unity.
#if UNITY_EDITOR
        // Se estiver no Editor, encerra o modo de execu��o.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Se estiver em um build do jogo, fecha o aplicativo.
        Application.Quit();
#endif
    }
}
