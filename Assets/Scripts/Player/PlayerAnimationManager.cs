using UnityEngine;

// Classe responsável por gerenciar as animações do jogador.
// Este script deve ser anexado ao GameObject do jogador que possui um componente Animator.
public class PlayerAnimationManager : MonoBehaviour
{
    // Referência ao script IsGroundedChecker que verifica se o jogador está no chão.
    private IsGroundedChecker groundedChecker;

    // Referência ao componente Animator que controla as animações do jogador.
    private Animator animator;

    // Referência ao script Health do jogador para gerenciar as animações de dano e morte.
    private Health playerHealth;

    // Método chamado automaticamente pela Unity quando o objeto é inicializado.
    // Aqui inicializamos as referências aos componentes necessários.
    private void Awake()
    {
        // Obtém o componente Animator anexado ao GameObject.
        animator = GetComponent<Animator>();

        // Obtém o componente IsGroundedChecker para verificar o estado do jogador em relação ao chão.
        groundedChecker = GetComponent<IsGroundedChecker>();

        // Obtém o componente Health para poder gerenciar as animações de dano e morte.
        playerHealth = GetComponent<Health>();

        // Inscreve os métodos de animação nos eventos de dano e morte do jogador.
        playerHealth.OnHurt += PlayHurtAnimation;
        playerHealth.OnDead += PlayDeathAnimation;
    }

    // Método chamado automaticamente pela Unity no início da execução.
    // Aqui, configuramos os eventos necessários, como ataques.
    private void Start()
    {
        // Inscreve o método de animação de ataque no evento OnAttack do InputManager.
        GameManager.Instance.InputManager.OnAttack += PlayAttackAnimation;
    }

    // Método chamado automaticamente pela Unity a cada quadro (frame).
    // Atualiza os parâmetros das animações com base no estado do jogador.
    private void Update()
    {
        // Verifica se o jogador está se movendo.
        // Isso é determinado pelo valor do movimento horizontal obtido pelo InputManager.
        // Se o valor for diferente de 0, significa que o jogador está se movendo.
        bool isMoving = GameManager.Instance.InputManager.Movement != 0;

        // Define o parâmetro "isMoving" no Animator.
        // Esse parâmetro é usado para acionar ou parar a animação de movimento no controlador de animação.
        animator.SetBool("isMoving", isMoving);

        // Define o parâmetro "isJumping" no Animator.
        // Esse parâmetro é verdadeiro se o jogador não estiver no chão (baseado no IsGroundedChecker).
        // Isso é usado para ativar a animação de pulo ou queda.
        animator.SetBool("isJumping", !groundedChecker.IsGrounded());
    }

    // Método chamado quando o jogador é atingido por dano.
    private void PlayHurtAnimation()
    {
        // Aciona a animação de dor.
        animator.SetTrigger("hurt");
    }

    // Método chamado quando o jogador morre.
    private void PlayDeathAnimation()
    {
        // Aciona a animação de morte.
        animator.SetTrigger("death");
    }

    // Método chamado quando o jogador realiza um ataque.
    private void PlayAttackAnimation()
    {
        // Aciona a animação de ataque.
        animator.SetTrigger("attack");
    }
}
