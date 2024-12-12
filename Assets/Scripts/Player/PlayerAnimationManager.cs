using UnityEngine;

// Classe respons�vel por gerenciar as anima��es do jogador.
// Este script deve ser anexado ao GameObject do jogador que possui um componente Animator.
public class PlayerAnimationManager : MonoBehaviour
{
    // Refer�ncia ao script IsGroundedChecker que verifica se o jogador est� no ch�o.
    private IsGroundedChecker groundedChecker;

    // Refer�ncia ao componente Animator que controla as anima��es do jogador.
    private Animator animator;

    // Refer�ncia ao script Health do jogador para gerenciar as anima��es de dano e morte.
    private Health playerHealth;

    // M�todo chamado automaticamente pela Unity quando o objeto � inicializado.
    // Aqui inicializamos as refer�ncias aos componentes necess�rios.
    private void Awake()
    {
        // Obt�m o componente Animator anexado ao GameObject.
        animator = GetComponent<Animator>();

        // Obt�m o componente IsGroundedChecker para verificar o estado do jogador em rela��o ao ch�o.
        groundedChecker = GetComponent<IsGroundedChecker>();

        // Obt�m o componente Health para poder gerenciar as anima��es de dano e morte.
        playerHealth = GetComponent<Health>();

        // Inscreve os m�todos de anima��o nos eventos de dano e morte do jogador.
        playerHealth.OnHurt += PlayHurtAnimation;
        playerHealth.OnDead += PlayDeathAnimation;
    }

    // M�todo chamado automaticamente pela Unity no in�cio da execu��o.
    // Aqui, configuramos os eventos necess�rios, como ataques.
    private void Start()
    {
        // Inscreve o m�todo de anima��o de ataque no evento OnAttack do InputManager.
        GameManager.Instance.InputManager.OnAttack += PlayAttackAnimation;
    }

    // M�todo chamado automaticamente pela Unity a cada quadro (frame).
    // Atualiza os par�metros das anima��es com base no estado do jogador.
    private void Update()
    {
        // Verifica se o jogador est� se movendo.
        // Isso � determinado pelo valor do movimento horizontal obtido pelo InputManager.
        // Se o valor for diferente de 0, significa que o jogador est� se movendo.
        bool isMoving = GameManager.Instance.InputManager.Movement != 0;

        // Define o par�metro "isMoving" no Animator.
        // Esse par�metro � usado para acionar ou parar a anima��o de movimento no controlador de anima��o.
        animator.SetBool("isMoving", isMoving);

        // Define o par�metro "isJumping" no Animator.
        // Esse par�metro � verdadeiro se o jogador n�o estiver no ch�o (baseado no IsGroundedChecker).
        // Isso � usado para ativar a anima��o de pulo ou queda.
        animator.SetBool("isJumping", !groundedChecker.IsGrounded());
    }

    // M�todo chamado quando o jogador � atingido por dano.
    private void PlayHurtAnimation()
    {
        // Aciona a anima��o de dor.
        animator.SetTrigger("hurt");
    }

    // M�todo chamado quando o jogador morre.
    private void PlayDeathAnimation()
    {
        // Aciona a anima��o de morte.
        animator.SetTrigger("death");
    }

    // M�todo chamado quando o jogador realiza um ataque.
    private void PlayAttackAnimation()
    {
        // Aciona a anima��o de ataque.
        animator.SetTrigger("attack");
    }
}
