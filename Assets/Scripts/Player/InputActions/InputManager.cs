using System;
using UnityEngine.InputSystem;

// Classe respons�vel por gerenciar os controles de entrada do jogador.
// Este gerenciador encapsula os comandos do jogador, organizando e reutilizando-os de forma mais eficiente.
public class InputManager
{
    // Objeto respons�vel por armazenar os controles do jogador.
    // Ele � baseado no sistema Input System, que permite mapear e gerenciar comandos de entrada de forma mais flex�vel.
    // O objeto � inicializado no construtor da classe.
    private readonly PlayerControlls playerControlls;

    // Propriedade que retorna o valor do movimento horizontal do jogador.
    // O valor � lido do mapa de controles, que � configurado usando o sistema Input System.
    // Esse valor ser� utilizado para mover o jogador para a esquerda ou direita.
    public float Movement => playerControlls.Player.Movement.ReadValue<float>();

    // Evento que ser� disparado quando o jogador pressionar o bot�o de pulo.
    // Outros scripts podem se inscrever nesse evento para reagir ao comando de pulo.
    public event Action OnJump;

    // Evento que ser� disparado quando o jogador pressionar o bot�o de ataque.
    // Outros scripts podem se inscrever nesse evento para reagir ao comando de ataque.
    public event Action OnAttack;

    // Construtor da classe InputManager.
    // Ele � chamado ao criar uma nova inst�ncia do InputManager.
    // Este construtor � respons�vel por inicializar os controles do jogador e ativ�-los para come�ar a receber inputs.
    public InputManager()
    {
        // Cria uma nova inst�ncia dos controles do jogador com base no mapeamento gerado pelo Input System.
        playerControlls = new PlayerControlls();

        // Ativa os controles, permitindo que o sistema comece a captar os inputs do jogador.
        playerControlls.Enable();

        // Adiciona callbacks aos eventos de a��es do jogador, para que, quando o jogador realizar as a��es (pular, atacar),
        // os m�todos correspondentes sejam chamados.
        playerControlls.Player.Jump.performed += OnJumpPerformed;
        playerControlls.Player.Attack.performed += OnAttackPerformed;
    }

    // M�todo chamado quando o jogador pressiona o bot�o de pulo.
    // Este m�todo dispara o evento `OnJump`, notificando os scripts inscritos para reagir ao comando de pulo.
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke(); // O operador `?.` verifica se h� algum inscrito no evento antes de dispar�-lo.
    }

    // M�todo chamado quando o jogador pressiona o bot�o de ataque.
    // Este m�todo dispara o evento `OnAttack`, notificando os scripts inscritos para reagir ao comando de ataque.
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke(); // O operador `?.` verifica se h� algum inscrito no evento antes de dispar�-lo.
    }

    // M�todo para desabilitar os controles do jogador.
    // Isso pode ser �til para pausar o jogo ou desativar os controles durante uma cutscene, por exemplo.
    public void DisablePlayerInput() => playerControlls.Player.Disable();
}
