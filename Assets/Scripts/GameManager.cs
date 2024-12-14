using System;
using UnityEngine;

// Classe responsável por gerenciar o estado geral do jogo e disponibilizar sistemas centrais.
// Implementa o padrão Singleton para garantir que apenas uma instância do GameManager exista no jogo.
public class GameManager : MonoBehaviour
{
    // Declaração de uma instância estática única para o GameManager.
    public static GameManager Instance;

    // Declaração de uma variável pública para o gerenciador de áudio do jogo.
    public AudioManager AudioManager;

    // Declaração de uma propriedade para gerenciar as entradas do jogador.
    public InputManager InputManager { get; private set; }

    // Declaração de um campo serializado para armazenar uma referência ao GameObject que representa a porta do chefe.
    [Header("Dynamic GameObjects")]
    [SerializeField] private GameObject bossDoor;

    // Declaração de uma variável pública para gerenciar a interface do usuário do jogo.
    [Header("Managers")]
    public UIManager UIManager;

    // Declaração de uma variável para armazenar o total de chaves no jogo.
    private int totalKeys;

    // Declaração de uma variável para rastrear quantas chaves ainda estão faltando.
    private int keysLeft;

    // Método chamado automaticamente pela Unity antes do método Start para inicializar o script.
    private void Awake()
    {
        // Verifica se já existe uma instância do GameManager ativa na cena.
        if (Instance != null)
        {
            // Destroi este objeto caso outra instância já exista, evitando duplicações no Singleton.
            Destroy(this.gameObject);
            return;
        }

        // Atribui a instância atual ao campo estático Instance, definindo este objeto como a instância única do GameManager.
        Instance = this;

        // Obtém o total de objetos do tipo Key na cena e atribui à variável totalKeys.
        totalKeys = FindObjectsByType<Key>(FindObjectsSortMode.None).Length;

        // Inicializa a variável keysLeft com o total de chaves encontradas.
        keysLeft = totalKeys;

        // Cria uma nova instância do InputManager para gerenciar as entradas do jogador.
        this.InputManager = new InputManager();

        // Atualiza o texto na interface do usuário para exibir o total de chaves e as chaves restantes.
        UIManager.UpdateKeysLeftText(totalKeys, keysLeft);
    }

    // Método chamado para atualizar a contagem de chaves restantes.
    public void UpdateKeysLeft()
    {
        // Decrementa o número de chaves restantes.
        keysLeft--;

        // Atualiza a interface do usuário com os valores atualizados de totalKeys e keysLeft.
        UIManager.UpdateKeysLeftText(totalKeys, keysLeft);

        // Verifica se todas as chaves foram coletadas.
        CheckAllKeysCollected();
    }

    // Método privado para verificar se todas as chaves foram coletadas.
    private void CheckAllKeysCollected()
    {
        // Verifica se o número de chaves restantes é menor ou igual a zero.
        if (keysLeft <= 0)
        {
            // Destrói o GameObject que representa a porta do chefe, permitindo a passagem.
            Destroy(bossDoor);
        }
    }
}
