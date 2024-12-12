using System.Collections;
using UnityEngine;

// Classe abstrata que serve como base para o comportamento de robôs.
// Utiliza o atributo [RequireComponent] para garantir que o componente Animator
// esteja presente no GameObject ao qual esta classe é anexada.
[RequireComponent(typeof(Animator))]
public abstract class RobotBehavior : MonoBehaviour
{
    // Referência ao componente Animator, usada para gerenciar animações do robô.
    protected Animator animator;

    // Referência ao componente AudioSource para reproduzir efeitos sonoros.
    protected AudioSource audioSource;

    // Referência ao componente Health para gerenciar os eventos de dano e morte.
    protected Health robotHealth;

    // Método chamado automaticamente pela Unity durante a inicialização do objeto.
    // Obtém e armazena as referências necessárias e inscreve métodos em eventos de Health.
    protected virtual void Awake()
    {
        // Obtém o componente Animator anexado ao GameObject.
        animator = GetComponent<Animator>();

        // Obtém o componente AudioSource anexado ao GameObject.
        audioSource = GetComponent<AudioSource>();

        // Obtém o componente Health anexado ao GameObject.
        robotHealth = GetComponent<Health>();

        // Inscreve os métodos de animação nos eventos correspondentes do Health.
        robotHealth.OnHurt += PlayHurtAnimation;
        robotHealth.OnDead += PlayDeathAnimation;
    }

    // Método abstrato que obriga as classes derivadas a implementar o comportamento de atualização.
    protected abstract void Update();

    // Método para tocar a animação de dano (hurt).
    private void PlayHurtAnimation()
    {
        animator.SetTrigger("hurt");
    }

    // Método para tocar a animação de morte (death) e iniciar o processo de destruição do robô.
    private void PlayDeathAnimation()
    {
        animator.SetTrigger("death");

        // Inicia uma coroutine para destruir o robô após a animação.
        StartCoroutine(DestroyRobot(1));
    }

    // Coroutine para aguardar um tempo antes de destruir o GameObject do robô.
    private IEnumerator DestroyRobot(int time)
    {
        // Aguarda o número de segundos especificado antes de prosseguir.
        yield return new WaitForSeconds(time);

        // Destroi o GameObject do robô.
        Destroy(this.gameObject);
    }
}
