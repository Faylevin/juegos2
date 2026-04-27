using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    
    public string nombreArchivo = "JuegoGuardado";
    public string nombreDirectorio = "Partidas";
    public GameData datosjuego;
    public static int record;
    public static string nombre;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Awake(){
	    LoadFile();	//Al cargar la escena del juego se cargan los datos almacenados
    }

    public void SaveToFile(){
        if(!Directory.Exists(nombreDirectorio)) Directory.CreateDirectory(nombreDirectorio);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create(nombreDirectorio + "/" + nombreArchivo + ".bin");
        GameData datosjuego = new GameData(ManagerDisparo.puntosPlayer, 5.1f, Navegacion.nombreJugador);
        formatter.Serialize(saveFile,datosjuego);
        saveFile.Close();
        Debug.Log("Guardado en " + Directory.GetCurrentDirectory().ToString() + "/Saves/" + nombreArchivo + ".bin");

    }
    
   public void LoadFile(){
    BinaryFormatter formatter = new BinaryFormatter();
    FileStream saveFile = File.Open(nombreDirectorio + "/" + nombreArchivo + ".bin", FileMode.Open);

    GameData loadData = (GameData)formatter.Deserialize(saveFile);
    saveFile.Close();

    Debug.Log("Datos Cargados *******");
    Debug.Log("Nombre " + loadData.nombre);
    Debug.Log("Puntos " + loadData.puntos);
    Debug.Log("Tiempo " + loadData.tiempo);

    record = loadData.puntos;
    nombre = loadData.nombre;
}

}
