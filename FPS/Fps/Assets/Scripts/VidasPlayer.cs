using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidasPlayer : MonoBehaviour
{
    public Image vidaPlayer;              // Imagen que representa la barra de vida
    private float anchoVidasPlayer;       // Ancho inicial de la barra
    public static int vida;               // Vidas actuales del jugador
    private bool haMuerto;                // Estado de muerte
    public GameObject gameOver;           // Pantalla de Game Over
    private const int vidasINI = 5;       // Vidas iniciales
    public static int puedePerderVida = 1; // Control para evitar perder vida repetidamente

    void Start()
    {
        // Guardamos el ancho inicial de la barra de vida
        anchoVidasPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidasINI;
        gameOver.SetActive(false); // Ocultamos pantalla de Game Over al inicio
    }

    public void TomarDano(int dano)
    {
        if (vida > 0 && puedePerderVida == 1)
        {
            puedePerderVida = 0; // Evita que se reste vida varias veces seguidas
            vida -= dano;        // Restamos la cantidad de daño
            DibujaVida(vida);    // Actualizamos la barra de vida

            if (vida <= 0 && !haMuerto)
            {
                haMuerto = true;
                StartCoroutine(EjecutaMuerte());
            }
        }
    }

    // Método para actualizar la barra de vida visualmente
    private void DibujaVida(int vida){
    RectTransform transformaImagen = vidaPlayer.GetComponent<RectTransform>();
    transformaImagen.sizeDelta = new Vector2((float)vida / (float)vidasINI * anchoVidasPlayer, transformaImagen.sizeDelta.y);
}

    IEnumerator EjecutaMuerte(){
        yield return new WaitForSeconds(2.1f);
        gameOver.SetActive(true);
    }

}
