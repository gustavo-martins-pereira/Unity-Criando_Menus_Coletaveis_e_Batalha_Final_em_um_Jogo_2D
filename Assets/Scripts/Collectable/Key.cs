using UnityEngine;

// Classe respons�vel por gerenciar o comportamento da chave no jogo.
// Quando o jogador colide com a chave, ela � coletada, e o contador de chaves restantes � atualizado.
public class Key : MonoBehaviour
{
    // M�todo chamado automaticamente pela Unity quando outro objeto entra no gatilho (trigger) deste objeto.
    // Par�metros:
    // - collision: O componente Collider2D do objeto que entrou no gatilho.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Atualiza o contador de chaves restantes atrav�s do GameManager.
        // Presume-se que o GameManager tenha o m�todo UpdateKeysLeft implementado para gerenciar a l�gica das chaves.
        GameManager.Instance.UpdateKeysLeft();

        // Destroi este objeto (a chave) para remov�-lo da cena.
        Destroy(this.gameObject);
    }
}
