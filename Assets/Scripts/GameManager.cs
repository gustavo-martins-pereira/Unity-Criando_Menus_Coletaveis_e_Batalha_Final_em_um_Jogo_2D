using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Classe respons�vel por gerenciar o estado geral do jogo e disponibilizar sistemas centrais.
// Implementa o padr�o Singleton para garantir que apenas uma inst�ncia do GameManager exista no jogo.
public class GameManager : MonoBehaviour
{
    // Inst�ncia est�tica �nica para o GameManager, usada para acessar o GameManager de forma global.
    public static GameManager Instance;

    // Refer�ncia p�blica ao gerenciador de �udio do jogo, permitindo acesso e controle de sons e m�sica.
    public AudioManager AudioManager;

    // Propriedade que armazena uma inst�ncia do gerenciador de entradas do jogador.
    // A propriedade � somente leitura externamente, mas pode ser definida internamente.
    public InputManager InputManager { get; private set; }

    // Refer�ncia serializada para o GameObject que representa a porta do chefe.
    // Essas refer�ncias s�o atribu�das diretamente no inspetor do Unity.
    [Header("Dynamic GameObjects")]
    [SerializeField] private GameObject bossDoor;

    // Refer�ncia ao comportamento do jogador.
    [SerializeField] private PlayerBehavior player;

    // Refer�ncia ao comportamento do chefe.
    [SerializeField] private BossBehavior bossBehavior;

    // Refer�ncia ao componente respons�vel por detectar a entrada do jogador na �rea de ativa��o da luta contra o chefe.
    [SerializeField] private BossFightTrigger bossFightTrigger;

    // Refer�ncia ao gerenciador da interface do usu�rio, respons�vel por atualizar elementos visuais no jogo.
    [Header("Managers")]
    public UIManager UIManager;

    // Vari�vel privada que armazena o n�mero total de chaves no jogo.
    private int totalKeys;

    // Vari�vel privada que armazena o n�mero de chaves restantes a serem coletadas.
    private int keysLeft;

    // M�todo chamado automaticamente pelo Unity antes do m�todo Start.
    // Usado para inicializar vari�veis e configurar o estado inicial do script.
    private void Awake()
    {
        // Verifica se j� existe uma inst�ncia do GameManager ativa na cena.
        if (Instance != null)
        {
            // Caso outra inst�ncia exista, este objeto � destru�do para garantir que apenas uma inst�ncia exista.
            Destroy(this.gameObject);
            return;
        }

        // Define a inst�ncia atual como a �nica inst�ncia do GameManager.
        Instance = this;

        // Obt�m todos os objetos do tipo Key na cena e atribui o n�mero total � vari�vel totalKeys.
        totalKeys = FindObjectsByType<Key>(FindObjectsSortMode.None).Length;

        // Inicializa a vari�vel keysLeft com o valor de totalKeys.
        keysLeft = totalKeys;

        // Cria uma nova inst�ncia do InputManager, respons�vel por gerenciar as entradas do jogador.
        this.InputManager = new InputManager();

        // Subscri��o aos eventos de morte do jogador e do chefe para exibir as interfaces correspondentes.
        player.GetComponent<Health>().OnDead += HandleGameOver;
        bossBehavior.GetComponent<Health>().OnDead += HandleVictory;
    }

    // M�todo chamado pelo Unity uma vez durante a inicializa��o do jogo.
    // Inicializa os textos da interface e registra eventos adicionais.
    private void Start()
    {
        // Atualiza o texto da interface do usu�rio para exibir o total de chaves e o n�mero de chaves restantes.
        UIManager.UpdateKeysLeftText(totalKeys, keysLeft);

        // Inscreve o m�todo ActivateBossBehavior ao evento OnPlayerEnterBossFight do BossFightTrigger.
        // Isso garante que o comportamento do chefe seja ativado quando o jogador entrar na �rea de ativa��o.
        bossFightTrigger.OnPlayerEnterBossFight += ActivateBossBehavior;
    }

    // M�todo chamado quando o evento OnPlayerEnterBossFight � disparado.
    // Ativa o comportamento do chefe, iniciando a persegui��o ao jogador.
    private void ActivateBossBehavior()
    {
        bossBehavior.StartChasing();
    }

    // M�todo chamado quando o jogador morre.
    // Exibe o painel de Game Over na interface do usu�rio.
    private void HandleGameOver()
    {
        UIManager.OpenGamerOverPanel();
    }

    // M�todo chamado quando o chefe morre.
    // Exibe o texto de vit�ria e inicia a transi��o para a cena de cr�ditos.
    private void HandleVictory()
    {
        UIManager.ShowVictoryText();
        StartCoroutine(GoToCreditsScene());
    }

    // Corrotina que aguarda alguns segundos antes de carregar a cena de cr�ditos.
    private IEnumerator GoToCreditsScene()
    {
        // Aguarda 3 segundos antes de continuar.
        yield return new WaitForSeconds(3f);

        // Carrega a cena de cr�ditos.
        SceneManager.LoadScene("Credits");
    }

    // M�todo p�blico chamado para atualizar a contagem de chaves restantes no jogo.
    public void UpdateKeysLeft()
    {
        // Decrementa o n�mero de chaves restantes.
        keysLeft--;

        // Atualiza o texto na interface do usu�rio com os valores atualizados de totalKeys e keysLeft.
        UIManager.UpdateKeysLeftText(totalKeys, keysLeft);

        // Verifica se todas as chaves foram coletadas.
        CheckAllKeysCollected();
    }

    // M�todo privado que verifica se todas as chaves foram coletadas pelo jogador.
    private void CheckAllKeysCollected()
    {
        // Se o n�mero de chaves restantes for menor ou igual a zero, significa que todas as chaves foram coletadas.
        if (keysLeft <= 0)
        {
            // Destroi o GameObject que representa a porta do chefe, permitindo ao jogador acessar a pr�xima �rea.
            Destroy(bossDoor);
        }
    }

    // M�todo p�blico para obter a refer�ncia ao comportamento do jogador.
    // Retorna o objeto PlayerBehavior armazenado no campo player.
    public PlayerBehavior GetPlayer()
    {
        return player;
    }
}
