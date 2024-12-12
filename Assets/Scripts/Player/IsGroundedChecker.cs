using UnityEngine;

// Classe respons�vel por verificar se o jogador ou objeto est� no ch�o (grounded).
// Este script � anexado a um GameObject e utiliza um OverlapBox para detectar colis�es com o solo.
public class IsGroundedChecker : MonoBehaviour
{
    // Refer�ncia ao Transform usado como ponto central para o OverlapBox.
    // [SerializeField] permite configurar este valor diretamente no Editor da Unity.
    [SerializeField] private new Transform transform;

    // Tamanho do box usado para verificar a colis�o com o ch�o.
    // Representado como um vetor 2D (largura e altura).
    [SerializeField] private Vector2 checkerSize;

    // LayerMask usado para filtrar quais camadas devem ser consideradas como "ch�o".
    // Por exemplo, somente camadas espec�ficas (como "Ground") podem ser detectadas.
    [SerializeField] private LayerMask layerMask;

    // M�todo que desenha uma representa��o visual do OverlapBox no Editor da Unity.
    // Este m�todo � chamado automaticamente pela Unity quando o GameObject est� selecionado.
    private void OnDrawGizmos()
    {
        // Caso o Transform n�o esteja configurado, o m�todo � encerrado para evitar erros.
        if (transform == null) return;

        // Define a cor do Gizmo com base no estado de grounded.
        // Se o objeto estiver no ch�o, o Gizmo ser� verde; caso contr�rio, ser� vermelho.
        Gizmos.color = IsGrounded() ? Color.green : Color.red;

        // Desenha um WireCube (cubo sem preenchimento) para representar o box usado na detec��o.
        Gizmos.DrawWireCube(transform.position, checkerSize);
    }

    // M�todo respons�vel por verificar se o objeto est� no ch�o.
    // Retorna verdadeiro (true) se o OverlapBox detectar colis�o com o "ch�o", caso contr�rio, retorna falso (false).
    public bool IsGrounded()
    {
        // O m�todo Physics2D.OverlapBox verifica se h� colis�o dentro de uma �rea retangular.
        // Par�metros:
        // - `transform.position`: Posi��o central do box.
        // - `checkerSize`: Tamanho do box.
        // - `0f`: Rota��o do box (neste caso, sem rota��o).
        // - `layerMask`: Filtro para considerar somente colis�es com camadas espec�ficas.
        return Physics2D.OverlapBox(transform.position, checkerSize, 0f, layerMask);
    }
}
