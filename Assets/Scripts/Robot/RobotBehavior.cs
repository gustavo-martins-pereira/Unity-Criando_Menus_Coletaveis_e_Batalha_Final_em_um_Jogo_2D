using System.Collections;
using UnityEngine;

// Classe abstrata que serve como base para o comportamento de robôs.
// Essa classe implementa funcionalidades comuns, como animações, sons e gerenciamento de vida,
// e define métodos abstratos para serem implementados pelas classes derivadas.
[RequireComponent(typeof(Animator))]
public abstract class RobotBehavior : MonoBehaviour
{
    // Referência ao componente Animator, utilizada para controlar as animações do robô.
    protected Animator animator;

    // Referência ao componente AudioSource, utilizada para reproduzir efeitos sonoros do robô.
    protected AudioSource audioSource;

    // Referência ao componente Health, utilizada para monitorar os eventos de dano e morte do robô.
    protected Health robotHealth;

    // Sistema de partículas que será exibido ao receber dano.
    [SerializeField] private ParticleSystem hitParticle;

    // Método chamado automaticamente pela Unity quando o objeto é inicializado.
    // Configura as referências para os componentes necessários e inscreve métodos em eventos de saúde.
    protected virtual void Awake()
    {
        // Obtém o componente Animator anexado ao objeto e armazena a referência para animações.
        animator = GetComponent<Animator>();

        // Obtém o componente AudioSource anexado ao objeto e armazena a referência para sons.
        audioSource = GetComponent<AudioSource>();

        // Obtém o componente Health anexado ao objeto e armazena a referência para eventos de vida.
        robotHealth = GetComponent<Health>();

        // Inscreve o método HandleHurt no evento OnHurt do componente Health.
        robotHealth.OnHurt += HandleHurt;

        // Inscreve o método HandleDeath no evento OnDead do componente Health.
        robotHealth.OnDead += HandleDeath;
    }

    // Método abstrato para atualização lógica.
    // Classes derivadas devem implementar este método para definir comportamentos específicos.
    protected abstract void Update();

    // Método chamado quando o robô recebe dano.
    // Reproduz a animação de dano e exibe as partículas de impacto.
    private void HandleHurt()
    {
        // Define o gatilho "hurt" no Animator para reproduzir a animação correspondente.
        animator.SetTrigger("hurt");

        // Reproduz o sistema de partículas de impacto.
        PlayHitParticle();
    }

    // Método chamado quando o robô morre.
    // Reproduz a animação de morte, exibe partículas de impacto e inicia o processo de destruição.
    private void HandleDeath()
    {
        // Define o gatilho "death" no Animator para reproduzir a animação correspondente.
        animator.SetTrigger("death");

        // Reproduz o sistema de partículas de impacto.
        PlayHitParticle();

        // Inicia a coroutine que destruirá o robô após um tempo definido.
        StartCoroutine(DestroyRobot(1));
    }

    // Coroutine responsável por destruir o objeto do robô após um tempo de espera.
    private IEnumerator DestroyRobot(int time)
    {
        // Aguarda pelo número de segundos especificado no parâmetro `time`.
        yield return new WaitForSeconds(time);

        // Destrói o GameObject ao qual este script está anexado.
        Destroy(this.gameObject);
    }

    // Método responsável por instanciar e reproduzir o sistema de partículas de impacto.
    private void PlayHitParticle()
    {
        // Instancia o sistema de partículas na posição e rotação do robô.
        ParticleSystem instantiatedParticle = Instantiate(hitParticle, transform.position, transform.rotation);

        // Reproduz as partículas instanciadas.
        instantiatedParticle.Play();
    }
}
