using UnityEngine;

// Classe responsável por gerenciar o comportamento da chave no jogo.
// Quando o jogador colide com a chave, ela é coletada, e o contador de chaves restantes é atualizado.
public class Key : MonoBehaviour
{
    // Método chamado automaticamente pela Unity quando outro objeto entra no gatilho (trigger) deste objeto.
    // Parâmetros:
    // - collision: O componente Collider2D do objeto que entrou no gatilho.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Atualiza o contador de chaves restantes através do GameManager.
        // Presume-se que o GameManager tenha o método UpdateKeysLeft implementado para gerenciar a lógica das chaves.
        GameManager.Instance.UpdateKeysLeft();

        // Destroi este objeto (a chave) para removê-lo da cena.
        Destroy(this.gameObject);
    }
}
