using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class VidasPlayer : MonoBehaviour
{
    public Image vidaPlayer;
    private float anchoVidasPlayer;
    public static int vida;
    private bool haMuerto;
    public GameObject gameOver;
    private const int vidaINI = 5;
    public static int puedePerderVida = 1;
    public TMP_Text txtPuntos;
    public TMP_Text txtRecord;
    public TMP_Text nombreR;
    public GameObject fm;

    void Start()
    {
        vidaPlayer = GameObject.Find("VidasPlayer").GetComponent<Image>();
        gameOver = GameObject.Find("Canvas").transform.Find("GameOver").gameObject;
        txtPuntos = GameObject.Find("txtPuntos").GetComponent<TMP_Text>();
        txtRecord = GameObject.Find("txtRecord").GetComponent<TMP_Text>();
        nombreR = GameObject.Find("txtNombre").GetComponent<TMP_Text>();
        fm = GameObject.Find("FileManager");

        Debug.Log("vidaPlayer: " + vidaPlayer);
        Debug.Log("gameOver: " + gameOver);
        Debug.Log("txtPuntos: " + txtPuntos);
        Debug.Log("txtRecord: " + txtRecord);
        Debug.Log("nombreR: " + nombreR);
        Debug.Log("fm: " + fm);

        anchoVidasPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidaINI;
        gameOver.SetActive(false);
        txtRecord.text = "Record: " + FileManager.record.ToString();
        nombreR.text = FileManager.nombre;
    }

    private void Update()
    {
        if (txtPuntos != null)
            txtPuntos.text = "Puntos: " + ManagerDisparo.puntosPlayer.ToString();
    }

    public void TomarDano(int dano)
    {
        if (vida > 0 && puedePerderVida == 1)
        {
            puedePerderVida = 0;
            vida -= dano;
            DibujaVida(vida);
        }
        if (vida <= 0 && !haMuerto)
        {
            haMuerto = true;
            if (ManagerDisparo.puntosPlayer > FileManager.record)
            {
                fm.GetComponent<FileManager>().SaveToFile();
            }
            StartCoroutine(EjecutaMuerte());
        }
    }

    private void DibujaVida(int vida)
    {
        RectTransform transformaImagen = vidaPlayer.GetComponent<RectTransform>();
        transformaImagen.sizeDelta = new Vector2(anchoVidasPlayer * (float)vida / (float)vidaINI, transformaImagen.sizeDelta.y);
    }

    IEnumerator EjecutaMuerte()
    {
        gameOver.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("menu");
    }
}