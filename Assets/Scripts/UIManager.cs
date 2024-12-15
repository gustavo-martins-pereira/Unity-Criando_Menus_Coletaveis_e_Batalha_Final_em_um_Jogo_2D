using TMPro;
using UnityEngine;

// Classe responsável por gerenciar a interface do usuário no jogo, controlando textos dinâmicos e painéis de UI.
public class UIManager : MonoBehaviour
{
    // Referência ao componente TextMeshProUGUI que exibe o texto com a quantidade de chaves restantes.
    [SerializeField] private TextMeshProUGUI keysText;

    // Referência ao componente TextMeshProUGUI que exibe o texto com a quantidade de vidas restantes.
    [SerializeField] private TextMeshProUGUI livesText;

    // Referência ao componente TextMeshProUGUI que exibe o texto de vitória.
    [SerializeField] private TextMeshProUGUI victoryText;

    // Cabeçalho para organizar os campos relacionados a painéis no inspetor do Unity.
    [Header("Panels")]

    // Referência ao painel de opções do jogo. Este painel é configurado no inspetor do Unity.
    [SerializeField] private GameObject OptionsPanel;

    // Referência ao painel de pausa do jogo. Este painel é configurado no inspetor do Unity.
    [SerializeField] private GameObject PausePanel;

    // Referência ao painel de "Game Over" do jogo. Este painel é configurado no inspetor do Unity.
    [SerializeField] private GameObject GameOverPanel;

    // Método chamado automaticamente durante a fase de inicialização do objeto.
    private void Awake()
    {
        // Desativa o texto de vitória ao iniciar o jogo.
        victoryText.gameObject.SetActive(false);

        // Desativa o painel de opções para garantir que ele não esteja visível no início do jogo.
        OptionsPanel.SetActive(false);

        // Desativa o painel de pausa para garantir que ele não esteja visível no início do jogo.
        PausePanel.SetActive(false);

        // Desativa o painel de "Game Over" para garantir que ele não esteja visível no início do jogo.
        GameOverPanel.SetActive(false);
    }

    // Método chamado automaticamente após o Awake para conectar eventos e configurar o comportamento inicial.
    private void Start()
    {
        // Conecta o evento OnMenuOpenClose do InputManager ao método OpenClosePauseMenu.
        GameManager.Instance.InputManager.OnMenuOpenClose += OpenClosePauseMenu;
    }

    // Método responsável por alternar a visibilidade do painel de pausa.
    public void OpenClosePauseMenu()
    {
        // Verifica se o painel de pausa está atualmente desativado.
        if (PausePanel.activeSelf == false)
        {
            // Ativa o painel de pausa.
            PausePanel.SetActive(true);
        }
        else
        {
            // Desativa o painel de pausa.
            PausePanel.SetActive(false);
        }
    }

    // Método responsável por abrir o painel de opções do jogo.
    public void OpenOptionsPanel()
    {
        // Ativa o painel de opções.
        OptionsPanel.SetActive(true);
    }

    // Método responsável por abrir o painel de "Game Over".
    public void OpenGamerOverPanel()
    {
        // Ativa o painel de "Game Over".
        GameOverPanel.SetActive(true);
    }

    // Método responsável por exibir o texto de vitória na tela.
    public void ShowVictoryText()
    {
        // Ativa o objeto de texto de vitória.
        victoryText.gameObject.SetActive(true);
    }

    // Método para atualizar o texto que exibe o número de chaves restantes e o número total de chaves.
    public void UpdateKeysLeftText(int totalValue, int leftValue)
    {
        // Atualiza o texto exibido com o formato "chaves restantes / total de chaves".
        keysText.text = $"{leftValue}/{totalValue}";
    }

    // Método para atualizar o texto que exibe o número de vidas restantes.
    public void UpdateLivesText(int lives)
    {
        // Atualiza o texto exibido com o número de vidas restantes.
        livesText.text = $"{lives}";
    }
}
