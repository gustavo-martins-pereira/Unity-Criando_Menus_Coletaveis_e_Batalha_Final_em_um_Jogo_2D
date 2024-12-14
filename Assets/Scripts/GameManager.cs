using UnityEngine;

// Classe responsável por gerenciar o estado geral do jogo e disponibilizar sistemas centrais.
// Implementa o padrão Singleton para garantir que apenas uma instância do GameManager exista no jogo.
public class GameManager : MonoBehaviour
{
    // Instância única da classe GameManager (Singleton).
    // Permite acesso global ao GameManager sem a necessidade de referências diretas.
    public static GameManager Instance;

    // Referência ao gerenciador de áudio do jogo.
    // Permite reproduzir sons e efeitos sonoros de forma centralizada.
    public AudioManager AudioManager;

    // Gerenciador de entradas (InputManager), que encapsula os controles do jogador.
    // Propriedade pública para leitura, mas somente o GameManager pode atribuí-la.
    public InputManager InputManager { get; private set; }

    [Header("Managers")]
    // Referência ao gerenciador de interface do usuário (UI).
    // Permite interações e atualizações na interface do jogo.
    public UIManager UIManager;

    // Método chamado automaticamente pela Unity quando o script é inicializado (antes do método Start).
    private void Awake()
    {
        // Verifica se uma instância do GameManager já existe.
        if (Instance != null)
        {
            // Caso exista, destrói o novo GameObject para evitar duplicações.
            Destroy(this.gameObject);
            return;
        }

        // Define esta instância como a única instância do GameManager.
        Instance = this;

        // Inicializa o InputManager, que gerencia as entradas do jogador.
        // Essa abordagem encapsula os controles e facilita futuras modificações nos mapeamentos de entrada.
        this.InputManager = new InputManager();
    }
}
