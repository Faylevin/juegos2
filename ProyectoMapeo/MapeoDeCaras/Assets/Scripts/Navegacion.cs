using UnityEngine;
using UnityEngine.SceneManagement;

public class Navegacion : MonoBehaviour
{
    // Ir a la escena "Jugar"
    public void IrAJugar()
    {
        SceneManager.LoadScene("Jugar");
    }

    // Ir a la escena "Guia"
    public void IrAGuia()
    {
        SceneManager.LoadScene("Guia");
    }


     public void IrMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Salir del juego
    public void Salir()
    {
        Application.Quit();

        // Solo para probar en Unity
        Debug.Log("Juego cerrado");
    }
}