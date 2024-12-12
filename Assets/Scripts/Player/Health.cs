using System;
using UnityEngine;

// Classe responsável pela gestão da saúde do jogador (ou de qualquer outro objeto com saúde).
// Este script deve ser anexado ao GameObject que representa o jogador ou outro objeto que tenha pontos de vida.
public class Health : MonoBehaviour
{
    // Número de vidas (pontos de vida) que o jogador ou objeto possui.
    [SerializeField] private int lives;

    // Evento acionado quando o jogador morre (lives <= 0).
    public event Action OnDead;

    // Evento acionado quando o jogador leva dano, mas ainda tem vida restante.
    public event Action OnHurt;

    // Método público para receber dano e diminuir a quantidade de vidas.
    // Este método pode ser chamado de outros scripts ou eventos quando o jogador receber dano.
    public void TakeDamage()
    {
        // Reduz a quantidade de vidas do jogador.
        lives--;

        // Chama o método que lida com as consequências de ter levado dano.
        HandleDamageTaken();
    }

    // Método privado que lida com as consequências após o dano ser recebido.
    // Se as vidas acabaram, o evento de morte é acionado, senão, o evento de dano é acionado.
    private void HandleDamageTaken()
    {
        // Verifica se as vidas do jogador chegaram a 0 ou menos.
        if (lives <= 0)
        {
            // Se sim, aciona o evento OnDead, indicando que o jogador morreu.
            OnDead?.Invoke();
        }
        else
        {
            // Caso contrário, aciona o evento OnHurt, indicando que o jogador foi ferido.
            OnHurt?.Invoke();
        }
    }
}
