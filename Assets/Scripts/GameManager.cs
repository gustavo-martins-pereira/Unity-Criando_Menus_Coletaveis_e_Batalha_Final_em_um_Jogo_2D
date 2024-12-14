using UnityEngine;

// Classe respons�vel por gerenciar o estado geral do jogo e disponibilizar sistemas centrais.
// Implementa o padr�o Singleton para garantir que apenas uma inst�ncia do GameManager exista no jogo.
public class GameManager : MonoBehaviour
{
    // Inst�ncia �nica da classe GameManager (Singleton).
    // Permite acesso global ao GameManager sem a necessidade de refer�ncias diretas.
    public static GameManager Instance;

    // Refer�ncia ao gerenciador de �udio do jogo.
    // Permite reproduzir sons e efeitos sonoros de forma centralizada.
    public AudioManager AudioManager;

    // Gerenciador de entradas (InputManager), que encapsula os controles do jogador.
    // Propriedade p�blica para leitura, mas somente o GameManager pode atribu�-la.
    public InputManager InputManager { get; private set; }

    [Header("Managers")]
    // Refer�ncia ao gerenciador de interface do usu�rio (UI).
    // Permite intera��es e atualiza��es na interface do jogo.
    public UIManager UIManager;

    // M�todo chamado automaticamente pela Unity quando o script � inicializado (antes do m�todo Start).
    private void Awake()
    {
        // Verifica se uma inst�ncia do GameManager j� existe.
        if (Instance != null)
        {
            // Caso exista, destr�i o novo GameObject para evitar duplica��es.
            Destroy(this.gameObject);
            return;
        }

        // Define esta inst�ncia como a �nica inst�ncia do GameManager.
        Instance = this;

        // Inicializa o InputManager, que gerencia as entradas do jogador.
        // Essa abordagem encapsula os controles e facilita futuras modifica��es nos mapeamentos de entrada.
        this.InputManager = new InputManager();
    }
}
