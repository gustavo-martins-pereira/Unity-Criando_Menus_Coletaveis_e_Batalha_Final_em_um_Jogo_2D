using UnityEngine;

// Classe que define o comportamento do chefe (Boss) no jogo.
public class BossBehavior : MonoBehaviour
{
    // Referências aos componentes do Boss.
    private new Rigidbody2D rigidbody; // Rigidbody para movimentação.
    private SpriteRenderer spriteRenderer; // Renderer para manipular a aparência do Boss.
    private Transform playerTransform; // Referência ao Transform do jogador.

    // Velocidade de movimento do Boss.
    [SerializeField] private float moveSpeed = 3f;

    [Header("Attack Properties")]
    // Propriedades de ataque.
    [SerializeField] private float attackRange = 1f; // Distância mínima para o ataque.
    [SerializeField] private float attackSize = 1f; // Tamanho da área de ataque.
    [SerializeField] private Vector3 attackOffset; // Offset para ajustar a posição do ataque.
    [SerializeField] private LayerMask attackMask; // Máscara que define os objetos atingíveis pelo ataque.
    private Vector3 attackPosition; // Posição calculada para o ataque.

    private bool canAttack = false; // Indica se o Boss pode atacar o jogador.

    // Referência à barra de saúde do Boss.
    private Health health;

    // Referência ao Animator para manipular as animações.
    private Animator animator;

    // Inicializa as referências no método Awake, que é chamado antes do Start.
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        // Assina eventos de saúde para tocar animações de dano e morte.
        health.OnHurt += PlayHurtAnimation;
        health.OnDead += PlayDeathAnimation;
    }

    // Obtém a referência ao jogador no método Start, após o GameManager ter sido inicializado.
    private void Start()
    {
        playerTransform = GameManager.Instance.GetPlayer().transform;
    }

    // Método público que ativa o comportamento de perseguição do Boss.
    public void StartChasing()
    {
        animator.SetBool("canChase", true);
    }

    // Método chamado para fazer o Boss seguir o jogador.
    public void FollowPlayer()
    {
        // Calcula a nova posição em direção ao jogador, mas mantendo a mesma altura.
        Vector2 target = new Vector2(playerTransform.position.x, transform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rigidbody.position, target, moveSpeed * Time.deltaTime);

        // Move o Boss para a nova posição.
        rigidbody.MovePosition(newPosition);

        // Verifica a distância entre o Boss e o jogador e ajusta o estado de ataque.
        CheckDistanceFromPlayer();

        // Atualiza a orientação do Boss para olhar em direção ao jogador.
        LookAtPlayer();
    }

    // Verifica a distância entre o Boss e o jogador.
    private void CheckDistanceFromPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(playerTransform.position, transform.position);

        // Define se o Boss pode atacar com base na distância.
        canAttack = distanceFromPlayer <= attackRange;
    }

    // Atualiza a orientação do Boss para olhar em direção ao jogador.
    private void LookAtPlayer()
    {
        if (transform.position.x > playerTransform.position.x)
        {
            spriteRenderer.flipX = true; // Inverte o sprite para olhar à esquerda.
        }
        else
        {
            spriteRenderer.flipX = false; // Mantém o sprite olhando à direita.
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
        // Calcula a posição do ataque com base no offset.
        attackPosition = transform.position;
        attackPosition += transform.right * attackOffset.x;
        attackPosition += transform.up * attackOffset.y;

        // Detecta colisões na área de ataque e aplica dano se um objeto atingível for encontrado.
        Collider2D collisionInfo = Physics2D.OverlapCircle(attackPosition, attackSize, attackMask);
        if (collisionInfo != null)
        {
            collisionInfo.GetComponent<Health>().TakeDamage();
        }
    }

    // Desenha gizmos no editor para visualizar a área de ataque.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition, attackSize);
    }

    // Toca a animação de dano.
    private void PlayHurtAnimation()
    {
        animator.SetTrigger("hurt");
    }

    // Toca a animação de morte.
    private void PlayDeathAnimation()
    {
        animator.SetTrigger("death");
    }
}
