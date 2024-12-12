using System;
using UnityEngine.InputSystem;

// Classe responsável por gerenciar os controles de entrada do jogador.
// Este gerenciador encapsula os comandos do jogador, organizando e reutilizando-os de forma mais eficiente.
public class InputManager
{
    // Objeto responsável por armazenar os controles do jogador.
    // Ele é baseado no sistema Input System, que permite mapear e gerenciar comandos de entrada de forma mais flexível.
    // O objeto é inicializado no construtor da classe.
    private readonly PlayerControlls playerControlls;

    // Propriedade que retorna o valor do movimento horizontal do jogador.
    // O valor é lido do mapa de controles, que é configurado usando o sistema Input System.
    // Esse valor será utilizado para mover o jogador para a esquerda ou direita.
    public float Movement => playerControlls.Player.Movement.ReadValue<float>();

    // Evento que será disparado quando o jogador pressionar o botão de pulo.
    // Outros scripts podem se inscrever nesse evento para reagir ao comando de pulo.
    public event Action OnJump;

    // Evento que será disparado quando o jogador pressionar o botão de ataque.
    // Outros scripts podem se inscrever nesse evento para reagir ao comando de ataque.
    public event Action OnAttack;

    // Construtor da classe InputManager.
    // Ele é chamado ao criar uma nova instância do InputManager.
    // Este construtor é responsável por inicializar os controles do jogador e ativá-los para começar a receber inputs.
    public InputManager()
    {
        // Cria uma nova instância dos controles do jogador com base no mapeamento gerado pelo Input System.
        playerControlls = new PlayerControlls();

        // Ativa os controles, permitindo que o sistema comece a captar os inputs do jogador.
        playerControlls.Enable();

        // Adiciona callbacks aos eventos de ações do jogador, para que, quando o jogador realizar as ações (pular, atacar),
        // os métodos correspondentes sejam chamados.
        playerControlls.Player.Jump.performed += OnJumpPerformed;
        playerControlls.Player.Attack.performed += OnAttackPerformed;
    }

    // Método chamado quando o jogador pressiona o botão de pulo.
    // Este método dispara o evento `OnJump`, notificando os scripts inscritos para reagir ao comando de pulo.
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke(); // O operador `?.` verifica se há algum inscrito no evento antes de dispará-lo.
    }

    // Método chamado quando o jogador pressiona o botão de ataque.
    // Este método dispara o evento `OnAttack`, notificando os scripts inscritos para reagir ao comando de ataque.
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke(); // O operador `?.` verifica se há algum inscrito no evento antes de dispará-lo.
    }

    // Método para desabilitar os controles do jogador.
    // Isso pode ser útil para pausar o jogo ou desativar os controles durante uma cutscene, por exemplo.
    public void DisablePlayerInput() => playerControlls.Player.Disable();
}
