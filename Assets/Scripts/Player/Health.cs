using System;
using UnityEngine;

// Classe respons�vel pela gest�o da sa�de do jogador (ou de qualquer outro objeto com sa�de).
// Este script deve ser anexado ao GameObject que representa o jogador ou outro objeto que tenha pontos de vida.
public class Health : MonoBehaviour
{
    // N�mero de vidas (pontos de vida) que o jogador ou objeto possui.
    [SerializeField] private int lives;

    // Evento acionado quando o jogador morre (lives <= 0).
    public event Action OnDead;

    // Evento acionado quando o jogador leva dano, mas ainda tem vida restante.
    public event Action OnHurt;

    // M�todo p�blico para receber dano e diminuir a quantidade de vidas.
    // Este m�todo pode ser chamado de outros scripts ou eventos quando o jogador receber dano.
    public void TakeDamage()
    {
        // Reduz a quantidade de vidas do jogador.
        lives--;

        // Chama o m�todo que lida com as consequ�ncias de ter levado dano.
        HandleDamageTaken();
    }

    // M�todo privado que lida com as consequ�ncias ap�s o dano ser recebido.
    // Se as vidas acabaram, o evento de morte � acionado, sen�o, o evento de dano � acionado.
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
            // Caso contr�rio, aciona o evento OnHurt, indicando que o jogador foi ferido.
            OnHurt?.Invoke();
        }
    }
}
