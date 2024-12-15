using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Classe responsável por gerenciar o estado geral do jogo e disponibilizar sistemas centrais.
// Implementa o padrão Singleton para garantir que apenas uma instância do GameManager exista no jogo.
public class GameManager : MonoBehaviour
{
    // Instância estática única para o GameManager, usada para acessar o GameManager de forma global.
    public static GameManager Instance;

    // Referência pública ao gerenciador de áudio do jogo, permitindo acesso e controle de sons e música.
    public AudioManager AudioManager;

    // Propriedade que armazena uma instância do gerenciador de entradas do jogador.
    // A propriedade é somente leitura externamente, mas pode ser definida internamente.
    public InputManager InputManager { get; private set; }

    // Referência serializada para o GameObject que representa a porta do chefe.
    // Essas referências são atribuídas diretamente no inspetor do Unity.
    [Header("Dynamic GameObjects")]
    [SerializeField] private GameObject bossDoor;

    // Referência ao comportamento do jogador.
    [SerializeField] private PlayerBehavior player;

    // Referência ao comportamento do chefe.
    [SerializeField] private BossBehavior bossBehavior;

    // Referência ao componente responsável por detectar a entrada do jogador na área de ativação da luta contra o chefe.
    [SerializeField] private BossFightTrigger bossFightTrigger;

    // Referência ao gerenciador da interface do usuário, responsável por atualizar elementos visuais no jogo.
    [Header("Managers")]
    public UIManager UIManager;

    // Variável privada que armazena o número total de chaves no jogo.
    private int totalKeys;

    // Variável privada que armazena o número de chaves restantes a serem coletadas.
    private int keysLeft;

    // Método chamado automaticamente pelo Unity antes do método Start.
    // Usado para inicializar variáveis e configurar o estado inicial do script.
    private void Awake()
    {
        // Verifica se já existe uma instância do GameManager ativa na cena.
        if (Instance != null)
        {
            // Caso outra instância exista, este objeto é destruído para garantir que apenas uma instância exista.
            Destroy(this.gameObject);
            return;
        }

        // Define a instância atual como a única instância do GameManager.
        Instance = this;

        // Obtém todos os objetos do tipo Key na cena e atribui o número total à variável totalKeys.
        totalKeys = FindObjectsByType<Key>(FindObjectsSortMode.None).Length;

        // Inicializa a variável keysLeft com o valor de totalKeys.
        keysLeft = totalKeys;

        // Cria uma nova instância do InputManager, responsável por gerenciar as entradas do jogador.
        this.InputManager = new InputManager();

        // Subscrição aos eventos de morte do jogador e do chefe para exibir as interfaces correspondentes.
        player.GetComponent<Health>().OnDead += HandleGameOver;
        bossBehavior.GetComponent<Health>().OnDead += HandleVictory;
    }

    // Método chamado pelo Unity uma vez durante a inicialização do jogo.
    // Inicializa os textos da interface e registra eventos adicionais.
    private void Start()
    {
        // Atualiza o texto da interface do usuário para exibir o total de chaves e o número de chaves restantes.
        UIManager.UpdateKeysLeftText(totalKeys, keysLeft);

        // Inscreve o método ActivateBossBehavior ao evento OnPlayerEnterBossFight do BossFightTrigger.
        // Isso garante que o comportamento do chefe seja ativado quando o jogador entrar na área de ativação.
        bossFightTrigger.OnPlayerEnterBossFight += ActivateBossBehavior;
    }

    // Método chamado quando o evento OnPlayerEnterBossFight é disparado.
    // Ativa o comportamento do chefe, iniciando a perseguição ao jogador.
    private void ActivateBossBehavior()
    {
        bossBehavior.StartChasing();
    }

    // Método chamado quando o jogador morre.
    // Exibe o painel de Game Over na interface do usuário.
    private void HandleGameOver()
    {
        UIManager.OpenGamerOverPanel();
    }

    // Método chamado quando o chefe morre.
    // Exibe o texto de vitória e inicia a transição para a cena de créditos.
    private void HandleVictory()
    {
        UIManager.ShowVictoryText();
        StartCoroutine(GoToCreditsScene());
    }

    // Corrotina que aguarda alguns segundos antes de carregar a cena de créditos.
    private IEnumerator GoToCreditsScene()
    {
        // Aguarda 3 segundos antes de continuar.
        yield return new WaitForSeconds(3f);

        // Carrega a cena de créditos.
        SceneManager.LoadScene("Credits");
    }

    // Método público chamado para atualizar a contagem de chaves restantes no jogo.
    public void UpdateKeysLeft()
    {
        // Decrementa o número de chaves restantes.
        keysLeft--;

        // Atualiza o texto na interface do usuário com os valores atualizados de totalKeys e keysLeft.
        UIManager.UpdateKeysLeftText(totalKeys, keysLeft);

        // Verifica se todas as chaves foram coletadas.
        CheckAllKeysCollected();
    }

    // Método privado que verifica se todas as chaves foram coletadas pelo jogador.
    private void CheckAllKeysCollected()
    {
        // Se o número de chaves restantes for menor ou igual a zero, significa que todas as chaves foram coletadas.
        if (keysLeft <= 0)
        {
            // Destroi o GameObject que representa a porta do chefe, permitindo ao jogador acessar a próxima área.
            Destroy(bossDoor);
        }
    }

    // Método público para obter a referência ao comportamento do jogador.
    // Retorna o objeto PlayerBehavior armazenado no campo player.
    public PlayerBehavior GetPlayer()
    {
        return player;
    }
}
