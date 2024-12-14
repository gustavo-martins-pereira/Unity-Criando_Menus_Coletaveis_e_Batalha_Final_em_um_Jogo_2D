using UnityEngine;

// Classe responsável por gerenciar a interface do usuário (UI) no jogo.
// Inclui a lógica para alternar o menu de pausa e abrir o painel de opções.
public class UIManager : MonoBehaviour
{
    // Headers são utilizados para organizar os campos no Inspector.
    [Header("Panels")]

    // Painel de opções do jogo. Pode ser configurado no Inspector.
    [SerializeField] private GameObject OptionsPanel;

    // Painel de pausa do jogo. Pode ser configurado no Inspector.
    [SerializeField] private GameObject PausePanel;

    // Método chamado automaticamente pela Unity durante a inicialização do objeto.
    // Configura os painéis para ficarem desativados inicialmente.
    private void Awake()
    {
        // Garante que o painel de opções esteja invisível no início do jogo.
        OptionsPanel.SetActive(false);

        // Garante que o painel de pausa esteja invisível no início do jogo.
        PausePanel.SetActive(false);
    }

    // Método chamado automaticamente pela Unity após o método Awake.
    // Inscreve o método de abrir/fechar o menu de pausa no evento OnMenuOpenClose.
    private void Start()
    {
        // Conecta o evento OnMenuOpenClose do InputManager à função OpenClosePauseMenu.
        // Isso permite que o menu de pausa seja alternado com base na entrada do jogador.
        GameManager.Instance.InputManager.OnMenuOpenClose += OpenClosePauseMenu;
    }

    // Método responsável por alternar a visibilidade do painel de pausa.
    public void OpenClosePauseMenu()
    {
        // Verifica se o painel de pausa está desativado.
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
    // Pode ser chamado, por exemplo, a partir de um botão na interface do usuário.
    public void OpenOptionsPanel()
    {
        // Mensagem de depuração para confirmar a abertura do painel de opções.
        print("Set options to be opened");

        // Ativa o painel de opções, tornando-o visível.
        OptionsPanel.SetActive(true);
    }
}
