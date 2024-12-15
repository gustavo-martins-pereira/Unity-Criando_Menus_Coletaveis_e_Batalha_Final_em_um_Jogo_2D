using System;
using UnityEngine;

// Define que este componente requer obrigatoriamente a presença de um BoxCollider2D no GameObject ao qual está anexado.
[RequireComponent(typeof(BoxCollider2D))]
public class BossFightTrigger : MonoBehaviour
{
    // Evento acionado quando o jogador entra na área de gatilho para iniciar a luta contra o chefe.
    public event Action OnPlayerEnterBossFight;

    // Método chamado automaticamente pelo Unity quando outro Collider2D entra na área de colisão do BoxCollider2D anexado.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o Collider2D que entrou na área de gatilho pertence a um objeto com a tag "Player".
        if (other.CompareTag("Player"))
        {
            // Invoca o evento OnPlayerEnterBossFight, notificando todos os inscritos que o jogador entrou na área do chefe.
            OnPlayerEnterBossFight?.Invoke();
        }
    }
}
