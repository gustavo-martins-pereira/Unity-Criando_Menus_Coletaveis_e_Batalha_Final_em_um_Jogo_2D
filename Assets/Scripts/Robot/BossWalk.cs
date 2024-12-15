using UnityEngine;

// Comportamento do estado de caminhada do Boss.
public class BossWalk : StateMachineBehaviour
{
    private BossBehavior bossBehavior; // Refer�ncia ao script de comportamento do Boss.

    // Chamado quando o estado � iniciado na State Machine.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        // Obt�m o script BossBehavior associado ao Animator do Boss.
        bossBehavior = animator.GetComponent<BossBehavior>();
    }

    // Chamado a cada frame enquanto o estado est� ativo.
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Faz o Boss seguir o jogador.
        bossBehavior.FollowPlayer();

        // Verifica se o Boss est� dentro do alcance de ataque.
        if (bossBehavior.GetCanAttack())
        {
            // Ativa o gatilho para transi��o para o estado de ataque.
            animator.SetTrigger("attack");
        }
    }

    // Chamado quando o estado � finalizado na State Machine.
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reseta o gatilho de ataque para evitar problemas de repeti��o de anima��es.
        animator.ResetTrigger("attack");
    }

    // M�todos opcionais, descoment�veis se necess�rios:
    // OnStateMove pode ser usado para processar root motion.
    // OnStateIK pode ser usado para configurar Inverse Kinematics (IK).

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
