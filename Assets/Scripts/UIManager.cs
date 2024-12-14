using TMPro;
using UnityEngine;

// Classe respons�vel por gerenciar a interface do usu�rio (UI) no jogo.
// Inclui a l�gica para alternar o menu de pausa, abrir o painel de op��es e atualizar textos din�micos na interface.
public class UIManager : MonoBehaviour
{
    // Refer�ncia ao texto que exibe a quantidade de chaves restantes.
    [SerializeField] private TextMeshProUGUI keysText;

    // Refer�ncia ao texto que exibe a quantidade de vidas restantes.
    [SerializeField] private TextMeshProUGUI livesText;

    // Cabe�alho para organizar os campos de pain�is no Inspector.
    [Header("Panels")]

    // Refer�ncia ao painel de op��es do jogo, configur�vel no Inspector.
    [SerializeField] private GameObject OptionsPanel;

    // Refer�ncia ao painel de pausa do jogo, configur�vel no Inspector.
    [SerializeField] private GameObject PausePanel;

    // M�todo chamado automaticamente pela Unity durante a inicializa��o do objeto.
    // Configura os pain�is para ficarem desativados inicialmente.
    private void Awake()
    {
        // Garante que o painel de op��es esteja invis�vel ao iniciar o jogo.
        OptionsPanel.SetActive(false);

        // Garante que o painel de pausa esteja invis�vel ao iniciar o jogo.
        PausePanel.SetActive(false);
    }

    // M�todo chamado automaticamente pela Unity ap�s o m�todo Awake.
    // Inscreve o m�todo de abrir/fechar o menu de pausa no evento do InputManager.
    private void Start()
    {
        // Conecta o evento OnMenuOpenClose do InputManager ao m�todo OpenClosePauseMenu.
        // Permite alternar o menu de pausa com base na entrada do jogador.
        GameManager.Instance.InputManager.OnMenuOpenClose += OpenClosePauseMenu;
    }

    // M�todo respons�vel por alternar a visibilidade do painel de pausa.
    public void OpenClosePauseMenu()
    {
        // Verifica se o painel de pausa est� atualmente desativado.
        if (PausePanel.activeSelf == false)
        {
            // Ativa o painel de pausa, tornando-o vis�vel.
            PausePanel.SetActive(true);
        }
        else
        {
            // Desativa o painel de pausa, tornando-o invis�vel.
            PausePanel.SetActive(false);
        }
    }

    // M�todo respons�vel por abrir o painel de op��es.
    // Pode ser chamado a partir de um bot�o na interface do usu�rio.
    public void OpenOptionsPanel()
    {
        // Ativa o painel de op��es, tornando-o vis�vel.
        OptionsPanel.SetActive(true);
    }

    // M�todo para atualizar o texto que exibe a quantidade de chaves restantes e o total.
    // Par�metros:
    // - totalValue: O n�mero total de chaves.
    // - leftValue: O n�mero de chaves que ainda n�o foram coletadas.
    public void UpdateKeysLeftText(int totalValue, int leftValue)
    {
        // Define o texto no formato "restantes/total".
        keysText.text = $"{leftValue}/{totalValue}";
    }

    // M�todo para atualizar o texto que exibe a quantidade de vidas restantes.
    // Par�metros:
    // - lives: O n�mero de vidas restantes.
    public void UpdateLivesText(int lives)
    {
        // Define o texto como o n�mero de vidas restantes.
        livesText.text = $"{lives}";
    }
}
