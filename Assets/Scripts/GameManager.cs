using System;
using UnityEngine;

// Classe respons�vel por gerenciar o estado geral do jogo e disponibilizar sistemas centrais.
// Implementa o padr�o Singleton para garantir que apenas uma inst�ncia do GameManager exista no jogo.
public class GameManager : MonoBehaviour
{
    // Declara��o de uma inst�ncia est�tica �nica para o GameManager.
    public static GameManager Instance;

    // Declara��o de uma vari�vel p�blica para o gerenciador de �udio do jogo.
    public AudioManager AudioManager;

    // Declara��o de uma propriedade para gerenciar as entradas do jogador.
    public InputManager InputManager { get; private set; }

    // Declara��o de um campo serializado para armazenar uma refer�ncia ao GameObject que representa a porta do chefe.
    [Header("Dynamic GameObjects")]
    [SerializeField] private GameObject bossDoor;

    // Declara��o de uma vari�vel p�blica para gerenciar a interface do usu�rio do jogo.
    [Header("Managers")]
    public UIManager UIManager;

    // Declara��o de uma vari�vel para armazenar o total de chaves no jogo.
    private int totalKeys;

    // Declara��o de uma vari�vel para rastrear quantas chaves ainda est�o faltando.
    private int keysLeft;

    // M�todo chamado automaticamente pela Unity antes do m�todo Start para inicializar o script.
    private void Awake()
    {
        // Verifica se j� existe uma inst�ncia do GameManager ativa na cena.
        if (Instance != null)
        {
            // Destroi este objeto caso outra inst�ncia j� exista, evitando duplica��es no Singleton.
            Destroy(this.gameObject);
            return;
        }

        // Atribui a inst�ncia atual ao campo est�tico Instance, definindo este objeto como a inst�ncia �nica do GameManager.
        Instance = this;

        // Obt�m o total de objetos do tipo Key na cena e atribui � vari�vel totalKeys.
        totalKeys = FindObjectsByType<Key>(FindObjectsSortMode.None).Length;

        // Inicializa a vari�vel keysLeft com o total de chaves encontradas.
        keysLeft = totalKeys;

        // Cria uma nova inst�ncia do InputManager para gerenciar as entradas do jogador.
        this.InputManager = new InputManager();

        // Atualiza o texto na interface do usu�rio para exibir o total de chaves e as chaves restantes.
        UIManager.UpdateKeysLeftText(totalKeys, keysLeft);
    }

    // M�todo chamado para atualizar a contagem de chaves restantes.
    public void UpdateKeysLeft()
    {
        // Decrementa o n�mero de chaves restantes.
        keysLeft--;

        // Atualiza a interface do usu�rio com os valores atualizados de totalKeys e keysLeft.
        UIManager.UpdateKeysLeftText(totalKeys, keysLeft);

        // Verifica se todas as chaves foram coletadas.
        CheckAllKeysCollected();
    }

    // M�todo privado para verificar se todas as chaves foram coletadas.
    private void CheckAllKeysCollected()
    {
        // Verifica se o n�mero de chaves restantes � menor ou igual a zero.
        if (keysLeft <= 0)
        {
            // Destr�i o GameObject que representa a porta do chefe, permitindo a passagem.
            Destroy(bossDoor);
        }
    }
}
