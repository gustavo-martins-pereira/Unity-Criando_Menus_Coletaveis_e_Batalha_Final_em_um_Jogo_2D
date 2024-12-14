using UnityEngine;

// Classe responsável pelo comportamento do jogador, incluindo movimento, pulo e ataque
public class PlayerBehavior : MonoBehaviour
{
    // Velocidade de movimento horizontal do jogador
    [SerializeField] private float moveSpeed = 5;

    // Força aplicada no pulo do jogador
    [SerializeField] private float jumpForce = 3;

    // Cabeçalho para organizar as propriedades de ataque no inspetor
    [Header("Attack Properties")]

    // Alcance do ataque do jogador
    [SerializeField] private float attackRange = 1f;

    // Transform representando a posição de origem do ataque
    [SerializeField] private Transform attackPosition;

    // Máscara de layer para especificar quais objetos podem ser atingidos pelo ataque
    [SerializeField] private LayerMask attackLayer;

    // Referência ao componente Rigidbody2D anexado ao jogador
    private new Rigidbody2D rigidbody2D;

    // Referência ao script que verifica se o jogador está no chão
    private IsGroundedChecker isGroundedChecker;

    // Referência ao componente SpriteRenderer para manipular a orientação do sprite do jogador
    private SpriteRenderer spriteRenderer;

    // Referência ao componente Health que gerencia a saúde do jogador
    private Health health;

    // Método chamado ao inicializar o objeto no início da execução do jogo
    private void Awake()
    {
        // Obtém e armazena o componente Rigidbody2D do jogador
        rigidbody2D = GetComponent<Rigidbody2D>();

        // Obtém e armazena o componente IsGroundedChecker para verificação de chão
        isGroundedChecker = GetComponent<IsGroundedChecker>();

        // Obtém e armazena o componente SpriteRenderer para manipulação visual
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Obtém e armazena o componente Health para gerenciamento da saúde
        health = GetComponent<Health>();

        // Inscreve métodos para os eventos OnDead e OnHurt do componente Health
        health.OnDead += HandlePlayerDeath;
        health.OnHurt += HandleHurt;
    }

    // Método chamado uma vez no início do jogo
    private void Start()
    {
        // Inscreve o método HandleJump ao evento OnJump do InputManager
        GameManager.Instance.InputManager.OnJump += HandleJump;
    }

    // Método chamado a cada frame para atualizar o estado do jogador
    private void Update()
    {
        // Obtém o valor de entrada de movimento horizontal do jogador
        float moveDirection = GameManager.Instance.InputManager.Movement;

        // Calcula a nova velocidade horizontal baseada na entrada do jogador
        Vector2 vectorMoveDirection = new Vector2(moveDirection * moveSpeed, rigidbody2D.linearVelocity.y);

        // Aplica a nova velocidade ao Rigidbody2D
        rigidbody2D.linearVelocity = vectorMoveDirection;

        // Atualiza a orientação do sprite com base na direção do movimento
        if (vectorMoveDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (vectorMoveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    // Método para reproduzir o som de caminhada do jogador
    private void PlayWalkSound()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerWalk);
    }

    // Método responsável por realizar o pulo do jogador
    private void HandleJump()
    {
        // Verifica se o jogador está no chão antes de permitir o pulo
        if (!isGroundedChecker.IsGrounded()) return;

        // Aplica força para cima ao Rigidbody2D para executar o pulo
        rigidbody2D.linearVelocity += Vector2.up * jumpForce;

        // Reproduz o som do pulo
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerJump);
    }

    // Método que executa o ataque do jogador
    private void Attack()
    {
        // Obtém os objetos dentro do alcance do ataque com a máscara de layer especificada
        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);

        // Itera pelos objetos atingidos e aplica dano
        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            // Verifica se o objeto possui o componente Health e aplica dano
            if (hittedEnemy.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage();
            }
        }

        // Reproduz o som de ataque
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerAttack);
    }

    // Método usado para desenhar o alcance do ataque no editor
    private void OnDrawGizmos()
    {
        // Define a cor do Gizmo no editor
        Gizmos.color = Color.yellow;

        // Desenha o alcance do ataque como um círculo
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    // Método acionado quando o jogador é ferido
    private void HandleHurt()
    {
        // Reproduz o som de dano
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerHurt);

        // Atualiza o texto de vidas na interface do usuário
        GameManager.Instance.UIManager.UpdateLivesText(health.GetLives());
    }

    // Método acionado quando o jogador morre
    private void HandlePlayerDeath()
    {
        // Desativa o collider do jogador para evitar interações
        GetComponent<Collider2D>().enabled = false;

        // Congela o Rigidbody2D para evitar movimentação
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        // Desativa o controle do jogador
        GameManager.Instance.InputManager.DisablePlayerInput();

        // Reproduz o som de morte
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerDeath);
    }
}
