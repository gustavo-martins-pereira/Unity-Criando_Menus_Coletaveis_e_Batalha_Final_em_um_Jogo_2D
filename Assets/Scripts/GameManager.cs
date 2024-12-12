using UnityEngine;

// Classe responsável por gerenciar o estado geral do jogo e disponibilizar sistemas centrais.
// Implementa o padrão Singleton, permitindo que apenas uma instância do GameManager exista no jogo.
public class GameManager : MonoBehaviour
{
    // Instância única da classe GameManager (Singleton).
    public static GameManager Instance;

    // Referência ao gerenciador de áudio do jogo.
    public AudioManager AudioManager;

    // Gerenciador de entradas (InputManager), que encapsula os controles do jogador.
    // Propriedade pública para leitura, mas somente o GameManager pode atribuí-la.
    public InputManager InputManager { get; private set; }

    // Método chamado pela Unity quando o script é inicializado (antes do método Start).
    private void Awake()
    {
        // Verifica se já existe uma instância do GameManager.
        if (Instance != null)
        {
            // Caso exista, destrói o novo GameObject para garantir que apenas uma instância persista.
            Destroy(this.gameObject);
            return;
        }

        // Define esta instância como a única instância do GameManager.
        Instance = this;

        // Inicializa o InputManager, permitindo o gerenciamento de entradas do jogador.
        this.InputManager = new InputManager();
    }
}
