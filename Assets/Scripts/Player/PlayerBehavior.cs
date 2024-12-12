using UnityEngine;

// Classe responsável pelo comportamento do jogador, como o movimento horizontal e o pulo.
// Este script deve ser anexado ao GameObject do jogador na Unity.
public class PlayerBehavior : MonoBehaviour
{
    // Velocidade de movimento do jogador no eixo horizontal.
    // [SerializeField] permite que esta variável seja configurada no Editor da Unity
    // sem precisar torná-la pública, mantendo o princípio de encapsulamento.
    [SerializeField] private float moveSpeed = 5;

    // Força aplicada no pulo do jogador.
    // Também configurável diretamente pelo Editor da Unity.
    [SerializeField] private float jumpForce = 3;

    [Header("Attack Properties")]
    // Definindo o alcance do ataque do jogador.
    [SerializeField] private float attackRange = 1f;

    // Posição de origem do ataque, que é onde o alcance do ataque será medido.
    [SerializeField] private Transform attackPosition;

    // Layer que indica em quais objetos o ataque pode colidir.
    [SerializeField] private LayerMask attackLayer;

    // Referência ao componente Rigidbody2D anexado ao jogador.
    // O Rigidbody2D é responsável pela física do jogador, como movimento e colisões.
    private new Rigidbody2D rigidbody2D;

    // Referência ao script `IsGroundedChecker` que verifica se o jogador está no chão.
    // Este script ajuda a garantir que o jogador só possa pular se estiver no chão.
    private IsGroundedChecker isGroundedChecker;

    // Referência ao componente SpriteRenderer para manipular a orientação visual do jogador.
    // O SpriteRenderer controla a imagem do personagem na tela, incluindo sua direção.
    private SpriteRenderer spriteRenderer;

    private Health health;

    // Método chamado automaticamente pela Unity quando o objeto é inicializado.
    // Aqui, componentes essenciais do jogador são referenciados.
    private void Awake()
    {
        // Obtém o componente Rigidbody2D anexado ao GameObject.
        rigidbody2D = GetComponent<Rigidbody2D>();

        // Obtém o componente IsGroundedChecker para verificar se o jogador está no chão.
        isGroundedChecker = GetComponent<IsGroundedChecker>();

        // Obtém o componente SpriteRenderer para manipular a orientação visual do jogador.
        spriteRenderer = GetComponent<SpriteRenderer>();

        health = GetComponent<Health>();

        health.OnDead += HandlePlayerDeath;
        health.OnHurt += PlayHurtSound;
    }

    // Método chamado automaticamente pela Unity no início da execução do jogo.
    // Aqui, configuramos eventos ou inicializações que dependem de outras instâncias.
    private void Start()
    {
        // Inscreve o método `HandleJump` no evento OnJump do InputManager.
        // Isso significa que toda vez que o jogador pressionar o botão de pulo, este método será chamado.
        GameManager.Instance.InputManager.OnJump += HandleJump;
    }

    // Método chamado automaticamente pela Unity a cada quadro (frame).
    // Gerencia o movimento horizontal do jogador com base na entrada do jogador.
    private void Update()
    {
        // Obtém o valor de entrada horizontal do jogador a partir do InputManager.
        // O valor de moveDirection vai de -1 (esquerda) a 1 (direita), dependendo da entrada do jogador.
        float moveDirection = GameManager.Instance.InputManager.Movement;

        // Define a nova velocidade horizontal do jogador, mantendo a velocidade vertical.
        // Isso move o jogador para a esquerda ou direita com base no valor de moveDirection.
        Vector2 vectorMoveDirection = new Vector2(moveDirection * moveSpeed, rigidbody2D.linearVelocity.y);

        // Aplica o movimento no Rigidbody2D, alterando sua velocidade.
        rigidbody2D.linearVelocity = vectorMoveDirection;

        // Ajusta a orientação do sprite com base na direção do movimento.
        // O personagem vira para a direita (não inverte a imagem) se move para a direita.
        // Caso contrário, vira para a esquerda (inverte a imagem).
        if (vectorMoveDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (vectorMoveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void PlayWalkSound()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerWalk);
    }

    // Método responsável por gerenciar o pulo do jogador.
    private void HandleJump()
    {
        // Verifica se o jogador está no chão antes de permitir o pulo.
        // Se o jogador não estiver no chão, ele não pode pular.
        if (!isGroundedChecker.IsGrounded()) return;

        // Aplica uma força para cima no Rigidbody2D para realizar o pulo.
        // A força do pulo é controlada pela variável jumpForce.
        rigidbody2D.linearVelocity += Vector2.up * jumpForce;

        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerJump);
    }

    // Método que gerencia o ataque do jogador.
    private void Attack()
    {
        // Verifica colisores dentro do alcance do ataque e no layer especificado.
        // A função OverlapCircleAll retorna todos os objetos dentro de um círculo no mundo.
        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);

        // Aplica dano aos inimigos detectados.
        // Para cada inimigo atingido, verificamos se ele possui o componente Health.
        // Se o inimigo tiver o componente, chamamos o método TakeDamage() para aplicar dano.
        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            if (hittedEnemy.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage();
            }
        }

        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerAttack);
    }

    // Método visual para desenhar o alcance do ataque no Editor da Unity.
    // Isso ajuda a visualizar no Editor o alcance do ataque do jogador.
    private void OnDrawGizmos()
    {
        // Define a cor do Gizmo que será desenhado no Editor.
        Gizmos.color = Color.yellow;

        // Desenha um círculo de alcance do ataque no Editor.
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    private void PlayHurtSound()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerHurt);
    }

    // Método chamado quando o jogador morre.
    private void HandlePlayerDeath()
    {
        // Desativa o collider e congela o Rigidbody2D do jogador.
        // Isso impede que o jogador continue interagindo com o ambiente após a morte.
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        // Desativa os controles do jogador.
        // Isso impede que o jogador continue tentando se mover após a morte.
        GameManager.Instance.InputManager.DisablePlayerInput();

        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerDeath);
    }
}
