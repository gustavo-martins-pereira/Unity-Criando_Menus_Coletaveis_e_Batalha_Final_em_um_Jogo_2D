using UnityEngine;

// Classe que define o comportamento de um rob� de combate corpo a corpo (melee).
// Herda da classe base `RobotBehavior`, permitindo reutilizar funcionalidades compartilhadas entre diferentes tipos de rob�s.
public class MeleeRobot : RobotBehavior
{
    // Posi��o de detec��o usada para verificar a presen�a do jogador.
    // Configur�vel no Editor da Unity para ajustar onde o rob� detectar� o jogador.
    [SerializeField] private Transform detectPosition;

    // Tamanho da �rea de detec��o em forma de caixa.
    // Define a dimens�o da �rea onde o rob� detecta o jogador.
    [SerializeField] private Vector2 detectBoxSize;

    // M�scara de camada usada para identificar o jogador na detec��o.
    // Apenas objetos pertencentes � camada especificada ser�o detectados.
    [SerializeField] private LayerMask playerLayer;

    // Tempo de espera entre os ataques do rob�.
    // Define o intervalo m�nimo entre os ataques consecutivos.
    [SerializeField] private float attackCooldown = 1f;

    [Header("Audio properties")]
    [SerializeField] private AudioClip[] audioClips;

    // Temporizador para rastrear o tempo desde o �ltimo ataque.
    private float cooldownTimer;

    // M�todo chamado automaticamente pela Unity quando o objeto � inicializado.
    // Sobrescreve o m�todo da classe base `RobotBehavior`.
    protected override void Awake()
    {
        base.Awake();

        // Inscreve m�todos de �udio nos eventos de dano e morte do rob�.
        base.robotHealth.OnHurt += PlayHurtAudio;
        base.robotHealth.OnDead += PlayDeadAudio;
    }

    // M�todo sobrescrito da classe base `RobotBehavior`.
    // � chamado automaticamente pela Unity em cada frame para atualizar o comportamento do rob�.
    protected override void Update()
    {
        // Incrementa o temporizador com o tempo desde o �ltimo frame.
        cooldownTimer += Time.deltaTime;

        // Se o tempo de espera ainda n�o passou, sai do m�todo sem realizar nenhuma a��o.
        if (cooldownTimer < attackCooldown) return;

        // Verifica se o jogador est� dentro da �rea de detec��o.
        if (PlayerInSight())
        {
            // Aciona a anima��o de ataque se o jogador for detectado.
            animator.SetTrigger("attack");

            // Executa o ataque ao jogador.
            AttackPlayer();
        }
    }

    // M�todo chamado automaticamente pela Unity no Editor para desenhar gizmos.
    // �til para visualizar a �rea de detec��o durante o desenvolvimento.
    private void OnDrawGizmos()
    {
        // Garante que a posi��o de detec��o est� configurada antes de desenhar os gizmos.
        if (detectPosition == null) return;

        // Define a cor dos gizmos como vermelho para indicar a �rea de detec��o.
        Gizmos.color = Color.red;

        // Desenha uma caixa em forma de fio (wireframe) representando a �rea de detec��o.
        Gizmos.DrawWireCube(detectPosition.position, detectBoxSize);
    }

    // M�todo respons�vel por realizar o ataque ao jogador.
    private void AttackPlayer()
    {
        // Configura o �udio de ataque e reinicia o temporizador de cooldown.
        audioSource.clip = audioClips[0];
        audioSource.Play();

        // Reinicia o temporizador de cooldown para controlar o intervalo entre ataques.
        cooldownTimer = 0;

        // Verifica se h� um jogador na �rea de detec��o e tenta acessar o componente `Health` dele.
        if (CheckPlayerInDetectArea().TryGetComponent(out Health playerHealth))
        {
            // Se o jogador foi encontrado e possui o componente `Health`, aplica dano a ele.
            playerHealth.TakeDamage();
        }
    }

    // M�todo auxiliar para verificar a presen�a do jogador na �rea de detec��o.
    // Retorna o colisor do jogador se encontrado, ou `null` caso contr�rio.
    private Collider2D CheckPlayerInDetectArea()
    {
        return Physics2D.OverlapBox(detectPosition.position, detectBoxSize, 0f, playerLayer);
    }

    // M�todo para verificar se o jogador est� na �rea de detec��o.
    // Retorna `true` se o jogador foi detectado e `false` caso contr�rio.
    private bool PlayerInSight()
    {
        // Utiliza o m�todo auxiliar para verificar a presen�a do jogador na �rea de detec��o.
        Collider2D playerCollider = CheckPlayerInDetectArea();

        // Retorna verdadeiro se o jogador foi detectado, falso caso contr�rio.
        return playerCollider != null;
    }

    // M�todo chamado quando o rob� sofre dano, reproduzindo o �udio correspondente.
    private void PlayHurtAudio()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    // M�todo chamado quando o rob� morre, reproduzindo o �udio correspondente.
    private void PlayDeadAudio()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }
}
