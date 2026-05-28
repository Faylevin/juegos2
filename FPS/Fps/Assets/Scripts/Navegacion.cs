using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using TMPro;

public class Navegacion : MonoBehaviour
{
    public string nombreArchivo = "JuegoGuardado";
    public string nombreDirectorio = "Partidas";
    public GameData datosjuego;

    public static string nombreJugador = " ";

    private void Awake(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        if(!Directory.Exists(nombreDirectorio))
        {
            Directory.CreateDirectory(nombreDirectorio);
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create(nombreDirectorio + "/" + nombreArchivo + ".bin");

        GameData datos = new GameData(
            ManagerDisparo.puntosPlayer,
            5.1f,
            Navegacion.nombreJugador
        );

        formatter.Serialize(saveFile, datos);
        saveFile.Close();

        Debug.Log("Guardado en " + Directory.GetCurrentDirectory() + "/" + nombreArchivo + ".bin");
    }

    public void IrJuego()
    {
        nombreJugador = GameObject.Find("txtNombre")
            .GetComponent<TMP_InputField>().text;

        SceneManager.LoadScene("Playground");

    }
}