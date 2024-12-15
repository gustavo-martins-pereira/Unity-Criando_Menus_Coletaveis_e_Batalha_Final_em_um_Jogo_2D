using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsBehavior : MonoBehaviour
{
    // Método público para retornar ao menu.
    // Pode ser vinculado a um botão na interface do Unity.
    public void GoToMenu()
    {
        // Carrega a cena chamada "Menu".
        SceneManager.LoadScene("Menu");
    }
}
