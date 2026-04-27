using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VidasPlayer : MonoBehaviour
{
    public Image vidaPlayer;              // Imagen que representa la barra de vida
    private float anchoVidasPlayer;       // Ancho inicial de la barra
    public static int vida;               // Vidas actuales del jugador
    private bool haMuerto;                // Estado de muerte
    public GameObject gameOver;           // Pantalla de Game Over
    private const int vidasINI = 5;       // Vidas iniciales
    public static int puedePerderVida = 1; // Control para evitar perder vida repetidamente
    public TMP_Text txtPuntos;
    public TMP_Text txtRecord;
    public TMP_Text nombreR;

    public GameObject fm;

 void Start()
{
    anchoVidasPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
    haMuerto = false;
    vida = vidasINI;
    gameOver.SetActive(false);

    // ← CAMBIO: Ahora es seguro porque FileManager ya cargó en Awake
    txtRecord.text = "Record: " + FileManager.record.ToString();
    nombreR.text = FileManager.nombre;
}


    private void Update(){
        txtPuntos.text = "Puntos: "  + ManagerDisparo.puntosPlayer.ToString();
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
                if(ManagerDisparo.puntosPlayer > FileManager.record){
                    fm.GetComponent<FileManager>().SaveToFile();
                }
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
        gameOver.SetActive(true);
        yield return new WaitForSeconds(1.2f);
         SceneManager.LoadScene("menu");
    }

}
