using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NetworkManager : MonoBehaviourPunCallbacks
{
   public Button btnMultiPlayer;
    void Start()
    {
        Debug.Log("Conexion a Servidor");
        PhotonNetwork.ConnectUsingSettings(); // Establece las caracteristicas de conexion
    }


    public override void OnConnectedToMaster() //Cuando se haga la conexion
    {
        Debug.Log("Unirse al Lobby");
        PhotonNetwork.JoinLobby();
    }


    public override void OnJoinedLobby()
    {
        Debug.Log("Preparados para juego multijugador");
        btnMultiPlayer.interactable=true;
    }


    public void EncuentraPartida()
    {
        Debug.Log("Buscando Sala");
        PhotonNetwork.JoinRandomRoom();
    }


    public override void OnJoinRandomFailed(short returnCode,string message)
    {
        CrearCuarto();
    }


    private void CrearCuarto()
    {
        int numCuarto = UnityEngine.Random.Range(0,23);
        RoomOptions opcionesSala = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 5,
            PublishUserId = true
        };
        PhotonNetwork.CreateRoom($"Cuarto_{numCuarto}",opcionesSala);
        Debug.Log($"Sala creada: {numCuarto}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Cargando escena");
        PhotonNetwork.LoadLevel("Online");
    }
   
}
