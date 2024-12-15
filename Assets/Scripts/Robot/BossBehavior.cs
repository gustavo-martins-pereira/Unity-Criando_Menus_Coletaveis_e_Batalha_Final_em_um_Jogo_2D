using UnityEngine;

// Classe que define o comportamento do chefe (Boss) no jogo.
public class BossBehavior : MonoBehaviour
{
    // Refer�ncias aos componentes do Boss.
    private new Rigidbody2D rigidbody; // Rigidbody para movimenta��o.
    private SpriteRenderer spriteRenderer; // Renderer para manipular a apar�ncia do Boss.
    private Transform playerTransform; // Refer�ncia ao Transform do jogador.

    // Velocidade de movimento do Boss.
    [SerializeField] private float moveSpeed = 3f;

    [Header("Attack Properties")]
    // Propriedades de ataque.
    [SerializeField] private float attackRange = 1f; // Dist�ncia m�nima para o ataque.
    [SerializeField] private float attackSize = 1f; // Tamanho da �rea de ataque.
    [SerializeField] private Vector3 attackOffset; // Offset para ajustar a posi��o do ataque.
    [SerializeField] private LayerMask attackMask; // M�scara que define os objetos ating�veis pelo ataque.
    private Vector3 attackPosition; // Posi��o calculada para o ataque.

    private bool canAttack = false; // Indica se o Boss pode atacar o jogador.

    // Refer�ncia � barra de sa�de do Boss.
    private Health health;

    // Refer�ncia ao Animator para manipular as anima��es.
    private Animator animator;

    // Inicializa as refer�ncias no m�todo Awake, que � chamado antes do Start.
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        // Assina eventos de sa�de para tocar anima��es de dano e morte.
        health.OnHurt += PlayHurtAnimation;
        health.OnDead += PlayDeathAnimation;
    }

    // Obt�m a refer�ncia ao jogador no m�todo Start, ap�s o GameManager ter sido inicializado.
    private void Start()
    {
        playerTransform = GameManager.Instance.GetPlayer().transform;
    }

    // M�todo p�blico que ativa o comportamento de persegui��o do Boss.
    public void StartChasing()
    {
        animator.SetBool("canChase", true);
    }

    // M�todo chamado para fazer o Boss seguir o jogador.
    public void FollowPlayer()
    {
        // Calcula a nova posi��o em dire��o ao jogador, mas mantendo a mesma altura.
        Vector2 target = new Vector2(playerTransform.position.x, transform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rigidbody.position, target, moveSpeed * Time.deltaTime);

        // Move o Boss para a nova posi��o.
        rigidbody.MovePosition(newPosition);

        // Verifica a dist�ncia entre o Boss e o jogador e ajusta o estado de ataque.
        CheckDistanceFromPlayer();

        // Atualiza a orienta��o do Boss para olhar em dire��o ao jogador.
        LookAtPlayer();
    }

    // Verifica a dist�ncia entre o Boss e o jogador.
    private void CheckDistanceFromPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(playerTransform.position, transform.position);

        // Define se o Boss pode atacar com base na dist�ncia.
        canAttack = distanceFromPlayer <= attackRange;
    }

    // Atualiza a orienta��o do Boss para olhar em dire��o ao jogador.
    private void LookAtPlayer()
    {
        if (transform.position.x > playerTransform.position.x)
        {
            spriteRenderer.flipX = true; // Inverte o sprite para olhar � esquerda.
        }
        else
        {
            spriteRenderer.flipX = false; // Mant�m o sprite olhando � direita.
        }
    }

    // Retorna o estado atual de possibilidade de ataque.
    public bool GetCanAttack()
    {
        return canAttack;
    }

    // Executa o ataque do Boss.
    private void Attack()
    {
        // Calcula a posi��o do ataque com base no offset.
        attackPosition = transform.position;
        attackPosition += transform.right * attackOffset.x;
        attackPosition += transform.up * attackOffset.y;

        // Detecta colis�es na �rea de ataque e aplica dano se um objeto ating�vel for encontrado.
        Collider2D collisionInfo = Physics2D.OverlapCircle(attackPosition, attackSize, attackMask);
        if (collisionInfo != null)
        {
            collisionInfo.GetComponent<Health>().TakeDamage();
        }
    }

    // Desenha gizmos no editor para visualizar a �rea de ataque.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition, attackSize);
    }

    // Toca a anima��o de dano.
    private void PlayHurtAnimation()
    {
        animator.SetTrigger("hurt");
    }

    // Toca a anima��o de morte.
    private void PlayDeathAnimation()
    {
        animator.SetTrigger("death");
    }
}
