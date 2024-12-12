using UnityEngine;

// Classe respons�vel por gerenciar o estado geral do jogo e disponibilizar sistemas centrais.
// Implementa o padr�o Singleton, permitindo que apenas uma inst�ncia do GameManager exista no jogo.
public class GameManager : MonoBehaviour
{
    // Inst�ncia �nica da classe GameManager (Singleton).
    public static GameManager Instance;

    // Refer�ncia ao gerenciador de �udio do jogo.
    public AudioManager AudioManager;

    // Gerenciador de entradas (InputManager), que encapsula os controles do jogador.
    // Propriedade p�blica para leitura, mas somente o GameManager pode atribu�-la.
    public InputManager InputManager { get; private set; }

    // M�todo chamado pela Unity quando o script � inicializado (antes do m�todo Start).
    private void Awake()
    {
        // Verifica se j� existe uma inst�ncia do GameManager.
        if (Instance != null)
        {
            // Caso exista, destr�i o novo GameObject para garantir que apenas uma inst�ncia persista.
            Destroy(this.gameObject);
            return;
        }

        // Define esta inst�ncia como a �nica inst�ncia do GameManager.
        Instance = this;

        // Inicializa o InputManager, permitindo o gerenciamento de entradas do jogador.
        this.InputManager = new InputManager();
    }
}
