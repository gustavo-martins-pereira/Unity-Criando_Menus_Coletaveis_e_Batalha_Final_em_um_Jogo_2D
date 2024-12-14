using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Classe respons�vel por gerenciar os controles de entrada do jogador.
// Este gerenciador encapsula os comandos do jogador, organizando e reutilizando-os de forma eficiente.
public class InputManager
{
    // Objeto que armazena os controles do jogador, utilizando o sistema Input System.
    // Ele � inicializado no construtor da classe e gerencia todas as a��es configuradas no mapa de controle.
    private readonly PlayerControlls playerControlls;

    // Propriedade que retorna o valor do movimento horizontal do jogador.
    // Esse valor ser� usado para controlar o deslocamento do jogador (esquerda/direita).
    public float Movement => playerControlls.Player.Movement.ReadValue<float>();

    // Eventos para a��es espec�ficas, permitindo que outros scripts se inscrevam para reagir a essas entradas.
    public event Action OnJump;          // Disparado quando o jogador pressiona o bot�o de pulo.
    public event Action OnAttack;        // Disparado quando o jogador pressiona o bot�o de ataque.
    public event Action OnMenuOpenClose; // Disparado ao abrir/fechar o menu.

    // Construtor da classe InputManager.
    // Inicializa os controles do jogador e registra callbacks para os eventos configurados no Input System.
    public InputManager()
    {
        // Instancia o mapa de controles do jogador gerado pelo Input System.
        playerControlls = new PlayerControlls();

        // Ativa o mapa de controles para come�ar a receber entradas do jogador.
        playerControlls.Enable();
        EnableUIInput();

        // Registra os callbacks para os eventos espec�ficos de pulo, ataque e abertura/fechamento do menu.
        playerControlls.Player.Jump.performed += OnJumpPerformed;
        playerControlls.Player.Attack.performed += OnAttackPerformed;
        playerControlls.UI.OpenCloseMenu.performed += OpenClosePauseMenuPerformed;
    }

    // M�todo para ativar os controles relacionados � interface do usu�rio (UI).
    // Isso permite que inputs destinados � UI sejam captados.
    public void EnableUIInput() => playerControlls.UI.Enable();

    // Callback disparado quando o jogador tenta abrir ou fechar o menu.
    // Este m�todo verifica se a cena atual � a de gameplay antes de disparar o evento `OnMenuOpenClose`.
    private void OpenClosePauseMenuPerformed(InputAction.CallbackContext obj)
    {
        if (SceneManager.GetActiveScene().name != "Gameplay") return;
        OnMenuOpenClose?.Invoke(); // Dispara o evento, se houver algum inscrito.
    }

    // Callback disparado quando o jogador pressiona o bot�o de pulo.
    // Este m�todo dispara o evento `OnJump` para notificar os scripts inscritos.
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke(); // O operador `?.` verifica se h� inscritos antes de disparar o evento.
    }

    // Callback disparado quando o jogador pressiona o bot�o de ataque.
    // Este m�todo dispara o evento `OnAttack` para notificar os scripts inscritos.
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }

    // M�todo para desabilitar os controles relacionados ao jogador.
    // Isso pode ser �til, por exemplo, para pausas no jogo ou cutscenes.
    public void DisablePlayerInput() => playerControlls.Player.Disable();
}
