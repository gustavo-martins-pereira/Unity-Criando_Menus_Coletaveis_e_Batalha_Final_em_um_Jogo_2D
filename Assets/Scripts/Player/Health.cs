using System;
using UnityEngine;

// Classe respons�vel pela gest�o da sa�de de um jogador ou objeto
public class Health : MonoBehaviour
{
    // Define a quantidade inicial de vidas do jogador ou objeto
    [SerializeField] private int lives;

    // Evento acionado quando a vida do jogador ou objeto chega a zero ou menos
    public event Action OnDead;

    // Evento acionado quando o jogador ou objeto sofre dano, mas ainda possui vidas restantes
    public event Action OnHurt;

    // M�todo para obter o valor atual de vidas
    public int GetLives()
    {
        // Retorna o n�mero atual de vidas
        return lives;
    }

    // M�todo p�blico chamado quando o jogador ou objeto sofre dano
    public void TakeDamage()
    {
        // Reduz o valor de vidas em 1
        lives--;

        // Chama o m�todo que gerencia os eventos de dano ou morte
        HandleDamageTaken();
    }

    // M�todo privado que verifica o estado de vidas e aciona os eventos correspondentes
    private void HandleDamageTaken()
    {
        // Verifica se o n�mero de vidas � menor ou igual a zero
        if (lives <= 0)
        {
            // Aciona o evento de morte caso n�o haja mais vidas
            OnDead?.Invoke();
        }
        else
        {
            // Aciona o evento de dano caso ainda existam vidas
            OnHurt?.Invoke();
        }
    }
}
