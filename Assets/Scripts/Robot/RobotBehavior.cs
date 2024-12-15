using System.Collections;
using UnityEngine;

// Classe abstrata que serve como base para o comportamento de rob�s.
// Essa classe implementa funcionalidades comuns, como anima��es, sons e gerenciamento de vida,
// e define m�todos abstratos para serem implementados pelas classes derivadas.
[RequireComponent(typeof(Animator))]
public abstract class RobotBehavior : MonoBehaviour
{
    // Refer�ncia ao componente Animator, utilizada para controlar as anima��es do rob�.
    protected Animator animator;

    // Refer�ncia ao componente AudioSource, utilizada para reproduzir efeitos sonoros do rob�.
    protected AudioSource audioSource;

    // Refer�ncia ao componente Health, utilizada para monitorar os eventos de dano e morte do rob�.
    protected Health robotHealth;

    // Sistema de part�culas que ser� exibido ao receber dano.
    [SerializeField] private ParticleSystem hitParticle;

    // M�todo chamado automaticamente pela Unity quando o objeto � inicializado.
    // Configura as refer�ncias para os componentes necess�rios e inscreve m�todos em eventos de sa�de.
    protected virtual void Awake()
    {
        // Obt�m o componente Animator anexado ao objeto e armazena a refer�ncia para anima��es.
        animator = GetComponent<Animator>();

        // Obt�m o componente AudioSource anexado ao objeto e armazena a refer�ncia para sons.
        audioSource = GetComponent<AudioSource>();

        // Obt�m o componente Health anexado ao objeto e armazena a refer�ncia para eventos de vida.
        robotHealth = GetComponent<Health>();

        // Inscreve o m�todo HandleHurt no evento OnHurt do componente Health.
        robotHealth.OnHurt += HandleHurt;

        // Inscreve o m�todo HandleDeath no evento OnDead do componente Health.
        robotHealth.OnDead += HandleDeath;
    }

    // M�todo abstrato para atualiza��o l�gica.
    // Classes derivadas devem implementar este m�todo para definir comportamentos espec�ficos.
    protected abstract void Update();

    // M�todo chamado quando o rob� recebe dano.
    // Reproduz a anima��o de dano e exibe as part�culas de impacto.
    private void HandleHurt()
    {
        // Define o gatilho "hurt" no Animator para reproduzir a anima��o correspondente.
        animator.SetTrigger("hurt");

        // Reproduz o sistema de part�culas de impacto.
        PlayHitParticle();
    }

    // M�todo chamado quando o rob� morre.
    // Reproduz a anima��o de morte, exibe part�culas de impacto e inicia o processo de destrui��o.
    private void HandleDeath()
    {
        // Define o gatilho "death" no Animator para reproduzir a anima��o correspondente.
        animator.SetTrigger("death");

        // Reproduz o sistema de part�culas de impacto.
        PlayHitParticle();

        // Inicia a coroutine que destruir� o rob� ap�s um tempo definido.
        StartCoroutine(DestroyRobot(1));
    }

    // Coroutine respons�vel por destruir o objeto do rob� ap�s um tempo de espera.
    private IEnumerator DestroyRobot(int time)
    {
        // Aguarda pelo n�mero de segundos especificado no par�metro `time`.
        yield return new WaitForSeconds(time);

        // Destr�i o GameObject ao qual este script est� anexado.
        Destroy(this.gameObject);
    }

    // M�todo respons�vel por instanciar e reproduzir o sistema de part�culas de impacto.
    private void PlayHitParticle()
    {
        // Instancia o sistema de part�culas na posi��o e rota��o do rob�.
        ParticleSystem instantiatedParticle = Instantiate(hitParticle, transform.position, transform.rotation);

        // Reproduz as part�culas instanciadas.
        instantiatedParticle.Play();
    }
}
