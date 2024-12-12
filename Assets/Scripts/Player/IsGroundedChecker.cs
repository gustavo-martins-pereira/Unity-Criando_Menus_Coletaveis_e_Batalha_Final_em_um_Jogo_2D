using UnityEngine;

// Classe responsável por verificar se o jogador ou objeto está no chão (grounded).
// Este script é anexado a um GameObject e utiliza um OverlapBox para detectar colisões com o solo.
public class IsGroundedChecker : MonoBehaviour
{
    // Referência ao Transform usado como ponto central para o OverlapBox.
    // [SerializeField] permite configurar este valor diretamente no Editor da Unity.
    [SerializeField] private new Transform transform;

    // Tamanho do box usado para verificar a colisão com o chão.
    // Representado como um vetor 2D (largura e altura).
    [SerializeField] private Vector2 checkerSize;

    // LayerMask usado para filtrar quais camadas devem ser consideradas como "chão".
    // Por exemplo, somente camadas específicas (como "Ground") podem ser detectadas.
    [SerializeField] private LayerMask layerMask;

    // Método que desenha uma representação visual do OverlapBox no Editor da Unity.
    // Este método é chamado automaticamente pela Unity quando o GameObject está selecionado.
    private void OnDrawGizmos()
    {
        // Caso o Transform não esteja configurado, o método é encerrado para evitar erros.
        if (transform == null) return;

        // Define a cor do Gizmo com base no estado de grounded.
        // Se o objeto estiver no chão, o Gizmo será verde; caso contrário, será vermelho.
        Gizmos.color = IsGrounded() ? Color.green : Color.red;

        // Desenha um WireCube (cubo sem preenchimento) para representar o box usado na detecção.
        Gizmos.DrawWireCube(transform.position, checkerSize);
    }

    // Método responsável por verificar se o objeto está no chão.
    // Retorna verdadeiro (true) se o OverlapBox detectar colisão com o "chão", caso contrário, retorna falso (false).
    public bool IsGrounded()
    {
        // O método Physics2D.OverlapBox verifica se há colisão dentro de uma área retangular.
        // Parâmetros:
        // - `transform.position`: Posição central do box.
        // - `checkerSize`: Tamanho do box.
        // - `0f`: Rotação do box (neste caso, sem rotação).
        // - `layerMask`: Filtro para considerar somente colisões com camadas específicas.
        return Physics2D.OverlapBox(transform.position, checkerSize, 0f, layerMask);
    }
}
