using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsBehavior : MonoBehaviour
{
    // M�todo p�blico para retornar ao menu.
    // Pode ser vinculado a um bot�o na interface do Unity.
    public void GoToMenu()
    {
        // Carrega a cena chamada "Menu".
        SceneManager.LoadScene("Menu");
    }
}
