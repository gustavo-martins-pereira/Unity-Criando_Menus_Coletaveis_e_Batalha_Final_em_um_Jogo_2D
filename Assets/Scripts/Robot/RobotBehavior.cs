using System.Collections;
using UnityEngine;

// Classe abstrata que serve como base para o comportamento de rob�s.
// Utiliza o atributo [RequireComponent] para garantir que o componente Animator
// esteja presente no GameObject ao qual esta classe � anexada.
[RequireComponent(typeof(Animator))]
public abstract class RobotBehavior : MonoBehaviour
{
    // Refer�ncia ao componente Animator, usada para gerenciar anima��es do rob�.
    protected Animator animator;

    // Refer�ncia ao componente AudioSource para reproduzir efeitos sonoros.
    protected AudioSource audioSource;

    // Refer�ncia ao componente Health para gerenciar os eventos de dano e morte.
    protected Health robotHealth;

    // M�todo chamado automaticamente pela Unity durante a inicializa��o do objeto.
    // Obt�m e armazena as refer�ncias necess�rias e inscreve m�todos em eventos de Health.
    protected virtual void Awake()
    {
        // Obt�m o componente Animator anexado ao GameObject.
        animator = GetComponent<Animator>();

        // Obt�m o componente AudioSource anexado ao GameObject.
        audioSource = GetComponent<AudioSource>();

        // Obt�m o componente Health anexado ao GameObject.
        robotHealth = GetComponent<Health>();

        // Inscreve os m�todos de anima��o nos eventos correspondentes do Health.
        robotHealth.OnHurt += PlayHurtAnimation;
        robotHealth.OnDead += PlayDeathAnimation;
    }

    // M�todo abstrato que obriga as classes derivadas a implementar o comportamento de atualiza��o.
    protected abstract void Update();

    // M�todo para tocar a anima��o de dano (hurt).
    private void PlayHurtAnimation()
    {
        animator.SetTrigger("hurt");
    }

    // M�todo para tocar a anima��o de morte (death) e iniciar o processo de destrui��o do rob�.
    private void PlayDeathAnimation()
    {
        animator.SetTrigger("death");

        // Inicia uma coroutine para destruir o rob� ap�s a anima��o.
        StartCoroutine(DestroyRobot(1));
    }

    // Coroutine para aguardar um tempo antes de destruir o GameObject do rob�.
    private IEnumerator DestroyRobot(int time)
    {
        // Aguarda o n�mero de segundos especificado antes de prosseguir.
        yield return new WaitForSeconds(time);

        // Destroi o GameObject do rob�.
        Destroy(this.gameObject);
    }
}
