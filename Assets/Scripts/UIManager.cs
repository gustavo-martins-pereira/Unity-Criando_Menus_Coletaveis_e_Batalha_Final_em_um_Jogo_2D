using TMPro;
using UnityEngine;

// Classe respons�vel por gerenciar a interface do usu�rio no jogo, controlando textos din�micos e pain�is de UI.
public class UIManager : MonoBehaviour
{
    // Refer�ncia ao componente TextMeshProUGUI que exibe o texto com a quantidade de chaves restantes.
    [SerializeField] private TextMeshProUGUI keysText;

    // Refer�ncia ao componente TextMeshProUGUI que exibe o texto com a quantidade de vidas restantes.
    [SerializeField] private TextMeshProUGUI livesText;

    // Refer�ncia ao componente TextMeshProUGUI que exibe o texto de vit�ria.
    [SerializeField] private TextMeshProUGUI victoryText;

    // Cabe�alho para organizar os campos relacionados a pain�is no inspetor do Unity.
    [Header("Panels")]

    // Refer�ncia ao painel de op��es do jogo. Este painel � configurado no inspetor do Unity.
    [SerializeField] private GameObject OptionsPanel;

    // Refer�ncia ao painel de pausa do jogo. Este painel � configurado no inspetor do Unity.
    [SerializeField] private GameObject PausePanel;

    // Refer�ncia ao painel de "Game Over" do jogo. Este painel � configurado no inspetor do Unity.
    [SerializeField] private GameObject GameOverPanel;

    // M�todo chamado automaticamente durante a fase de inicializa��o do objeto.
    private void Awake()
    {
        // Desativa o texto de vit�ria ao iniciar o jogo.
        victoryText.gameObject.SetActive(false);

        // Desativa o painel de op��es para garantir que ele n�o esteja vis�vel no in�cio do jogo.
        OptionsPanel.SetActive(false);

        // Desativa o painel de pausa para garantir que ele n�o esteja vis�vel no in�cio do jogo.
        PausePanel.SetActive(false);

        // Desativa o painel de "Game Over" para garantir que ele n�o esteja vis�vel no in�cio do jogo.
        GameOverPanel.SetActive(false);
    }

    // M�todo chamado automaticamente ap�s o Awake para conectar eventos e configurar o comportamento inicial.
    private void Start()
    {
        // Conecta o evento OnMenuOpenClose do InputManager ao m�todo OpenClosePauseMenu.
        GameManager.Instance.InputManager.OnMenuOpenClose += OpenClosePauseMenu;
    }

    // M�todo respons�vel por alternar a visibilidade do painel de pausa.
    public void OpenClosePauseMenu()
    {
        // Verifica se o painel de pausa est� atualmente desativado.
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

    // M�todo respons�vel por abrir o painel de op��es do jogo.
    public void OpenOptionsPanel()
    {
        // Ativa o painel de op��es.
        OptionsPanel.SetActive(true);
    }

    // M�todo respons�vel por abrir o painel de "Game Over".
    public void OpenGamerOverPanel()
    {
        // Ativa o painel de "Game Over".
        GameOverPanel.SetActive(true);
    }

    // M�todo respons�vel por exibir o texto de vit�ria na tela.
    public void ShowVictoryText()
    {
        // Ativa o objeto de texto de vit�ria.
        victoryText.gameObject.SetActive(true);
    }

    // M�todo para atualizar o texto que exibe o n�mero de chaves restantes e o n�mero total de chaves.
    public void UpdateKeysLeftText(int totalValue, int leftValue)
    {
        // Atualiza o texto exibido com o formato "chaves restantes / total de chaves".
        keysText.text = $"{leftValue}/{totalValue}";
    }

    // M�todo para atualizar o texto que exibe o n�mero de vidas restantes.
    public void UpdateLivesText(int lives)
    {
        // Atualiza o texto exibido com o n�mero de vidas restantes.
        livesText.text = $"{lives}";
    }
}
