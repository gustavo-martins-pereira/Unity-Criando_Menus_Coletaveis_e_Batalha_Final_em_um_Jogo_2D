using TMPro;
using UnityEngine;

// Classe responsável por gerenciar a interface do usuário (UI) no jogo.
// Inclui a lógica para alternar o menu de pausa, abrir o painel de opções e atualizar textos dinâmicos na interface.
public class UIManager : MonoBehaviour
{
    // Referência ao texto que exibe a quantidade de chaves restantes.
    [SerializeField] private TextMeshProUGUI keysText;

    // Referência ao texto que exibe a quantidade de vidas restantes.
    [SerializeField] private TextMeshProUGUI livesText;

    // Cabeçalho para organizar os campos de painéis no Inspector.
    [Header("Panels")]

    // Referência ao painel de opções do jogo, configurável no Inspector.
    [SerializeField] private GameObject OptionsPanel;

    // Referência ao painel de pausa do jogo, configurável no Inspector.
    [SerializeField] private GameObject PausePanel;

    // Método chamado automaticamente pela Unity durante a inicialização do objeto.
    // Configura os painéis para ficarem desativados inicialmente.
    private void Awake()
    {
        // Garante que o painel de opções esteja invisível ao iniciar o jogo.
        OptionsPanel.SetActive(false);

        // Garante que o painel de pausa esteja invisível ao iniciar o jogo.
        PausePanel.SetActive(false);
    }

    // Método chamado automaticamente pela Unity após o método Awake.
    // Inscreve o método de abrir/fechar o menu de pausa no evento do InputManager.
    private void Start()
    {
        // Conecta o evento OnMenuOpenClose do InputManager ao método OpenClosePauseMenu.
        // Permite alternar o menu de pausa com base na entrada do jogador.
        GameManager.Instance.InputManager.OnMenuOpenClose += OpenClosePauseMenu;
    }

    // Método responsável por alternar a visibilidade do painel de pausa.
    public void OpenClosePauseMenu()
    {
        // Verifica se o painel de pausa está atualmente desativado.
        if (PausePanel.activeSelf == false)
        {
            // Ativa o painel de pausa, tornando-o visível.
            PausePanel.SetActive(true);
        }
        else
        {
            // Desativa o painel de pausa, tornando-o invisível.
            PausePanel.SetActive(false);
        }
    }

    // Método responsável por abrir o painel de opções.
    // Pode ser chamado a partir de um botão na interface do usuário.
    public void OpenOptionsPanel()
    {
        // Ativa o painel de opções, tornando-o visível.
        OptionsPanel.SetActive(true);
    }

    // Método para atualizar o texto que exibe a quantidade de chaves restantes e o total.
    // Parâmetros:
    // - totalValue: O número total de chaves.
    // - leftValue: O número de chaves que ainda não foram coletadas.
    public void UpdateKeysLeftText(int totalValue, int leftValue)
    {
        // Define o texto no formato "restantes/total".
        keysText.text = $"{leftValue}/{totalValue}";
    }

    // Método para atualizar o texto que exibe a quantidade de vidas restantes.
    // Parâmetros:
    // - lives: O número de vidas restantes.
    public void UpdateLivesText(int lives)
    {
        // Define o texto como o número de vidas restantes.
        livesText.text = $"{lives}";
    }
}
