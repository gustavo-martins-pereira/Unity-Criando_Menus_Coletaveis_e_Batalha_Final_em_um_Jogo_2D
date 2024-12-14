using UnityEngine;

// Classe respons�vel por gerenciar a interface do usu�rio (UI) no jogo.
// Inclui a l�gica para alternar o menu de pausa e abrir o painel de op��es.
public class UIManager : MonoBehaviour
{
    // Headers s�o utilizados para organizar os campos no Inspector.
    [Header("Panels")]

    // Painel de op��es do jogo. Pode ser configurado no Inspector.
    [SerializeField] private GameObject OptionsPanel;

    // Painel de pausa do jogo. Pode ser configurado no Inspector.
    [SerializeField] private GameObject PausePanel;

    // M�todo chamado automaticamente pela Unity durante a inicializa��o do objeto.
    // Configura os pain�is para ficarem desativados inicialmente.
    private void Awake()
    {
        // Garante que o painel de op��es esteja invis�vel no in�cio do jogo.
        OptionsPanel.SetActive(false);

        // Garante que o painel de pausa esteja invis�vel no in�cio do jogo.
        PausePanel.SetActive(false);
    }

    // M�todo chamado automaticamente pela Unity ap�s o m�todo Awake.
    // Inscreve o m�todo de abrir/fechar o menu de pausa no evento OnMenuOpenClose.
    private void Start()
    {
        // Conecta o evento OnMenuOpenClose do InputManager � fun��o OpenClosePauseMenu.
        // Isso permite que o menu de pausa seja alternado com base na entrada do jogador.
        GameManager.Instance.InputManager.OnMenuOpenClose += OpenClosePauseMenu;
    }

    // M�todo respons�vel por alternar a visibilidade do painel de pausa.
    public void OpenClosePauseMenu()
    {
        // Verifica se o painel de pausa est� desativado.
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
    // Pode ser chamado, por exemplo, a partir de um bot�o na interface do usu�rio.
    public void OpenOptionsPanel()
    {
        // Mensagem de depura��o para confirmar a abertura do painel de op��es.
        print("Set options to be opened");

        // Ativa o painel de op��es, tornando-o vis�vel.
        OptionsPanel.SetActive(true);
    }
}
