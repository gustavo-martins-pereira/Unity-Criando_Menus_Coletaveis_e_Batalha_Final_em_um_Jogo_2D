using UnityEngine;

// Classe que define o comportamento de um robô de combate corpo a corpo (melee).
// Herda da classe base `RobotBehavior`, permitindo reutilizar funcionalidades compartilhadas entre diferentes tipos de robôs.
public class MeleeRobot : RobotBehavior
{
    // Posição de detecção usada para verificar a presença do jogador.
    // Configurável no Editor da Unity para ajustar onde o robô detectará o jogador.
    [SerializeField] private Transform detectPosition;

    // Tamanho da área de detecção em forma de caixa.
    // Define a dimensão da área onde o robô detecta o jogador.
    [SerializeField] private Vector2 detectBoxSize;

    // Máscara de camada usada para identificar o jogador na detecção.
    // Apenas objetos pertencentes à camada especificada serão detectados.
    [SerializeField] private LayerMask playerLayer;

    // Tempo de espera entre os ataques do robô.
    // Define o intervalo mínimo entre os ataques consecutivos.
    [SerializeField] private float attackCooldown = 1f;

    [Header("Audio properties")]
    [SerializeField] private AudioClip[] audioClips;

    // Temporizador para rastrear o tempo desde o último ataque.
    private float cooldownTimer;

    // Método chamado automaticamente pela Unity quando o objeto é inicializado.
    // Sobrescreve o método da classe base `RobotBehavior`.
    protected override void Awake()
    {
        base.Awake();

        // Inscreve métodos de áudio nos eventos de dano e morte do robô.
        base.robotHealth.OnHurt += PlayHurtAudio;
        base.robotHealth.OnDead += PlayDeadAudio;
    }

    // Método sobrescrito da classe base `RobotBehavior`.
    // É chamado automaticamente pela Unity em cada frame para atualizar o comportamento do robô.
    protected override void Update()
    {
        // Incrementa o temporizador com o tempo desde o último frame.
        cooldownTimer += Time.deltaTime;

        // Se o tempo de espera ainda não passou, sai do método sem realizar nenhuma ação.
        if (cooldownTimer < attackCooldown) return;

        // Verifica se o jogador está dentro da área de detecção.
        if (PlayerInSight())
        {
            // Aciona a animação de ataque se o jogador for detectado.
            animator.SetTrigger("attack");

            // Executa o ataque ao jogador.
            AttackPlayer();
        }
    }

    // Método chamado automaticamente pela Unity no Editor para desenhar gizmos.
    // Útil para visualizar a área de detecção durante o desenvolvimento.
    private void OnDrawGizmos()
    {
        // Garante que a posição de detecção está configurada antes de desenhar os gizmos.
        if (detectPosition == null) return;

        // Define a cor dos gizmos como vermelho para indicar a área de detecção.
        Gizmos.color = Color.red;

        // Desenha uma caixa em forma de fio (wireframe) representando a área de detecção.
        Gizmos.DrawWireCube(detectPosition.position, detectBoxSize);
    }

    // Método responsável por realizar o ataque ao jogador.
    private void AttackPlayer()
    {
        // Configura o áudio de ataque e reinicia o temporizador de cooldown.
        audioSource.clip = audioClips[0];
        audioSource.Play();

        // Reinicia o temporizador de cooldown para controlar o intervalo entre ataques.
        cooldownTimer = 0;

        // Verifica se há um jogador na área de detecção e tenta acessar o componente `Health` dele.
        if (CheckPlayerInDetectArea().TryGetComponent(out Health playerHealth))
        {
            // Se o jogador foi encontrado e possui o componente `Health`, aplica dano a ele.
            playerHealth.TakeDamage();
        }
    }

    // Método auxiliar para verificar a presença do jogador na área de detecção.
    // Retorna o colisor do jogador se encontrado, ou `null` caso contrário.
    private Collider2D CheckPlayerInDetectArea()
    {
        return Physics2D.OverlapBox(detectPosition.position, detectBoxSize, 0f, playerLayer);
    }

    // Método para verificar se o jogador está na área de detecção.
    // Retorna `true` se o jogador foi detectado e `false` caso contrário.
    private bool PlayerInSight()
    {
        // Utiliza o método auxiliar para verificar a presença do jogador na área de detecção.
        Collider2D playerCollider = CheckPlayerInDetectArea();

        // Retorna verdadeiro se o jogador foi detectado, falso caso contrário.
        return playerCollider != null;
    }

    // Método chamado quando o robô sofre dano, reproduzindo o áudio correspondente.
    private void PlayHurtAudio()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    // Método chamado quando o robô morre, reproduzindo o áudio correspondente.
    private void PlayDeadAudio()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }
}
