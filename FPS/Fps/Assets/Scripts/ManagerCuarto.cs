using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class ManagerCuarto : MonoBehaviour
{
    
    public static ManagerCuarto instanciaCompartida;

    private void Awake() {
        if (instanciaCompartida == null) {
            instanciaCompartida = this;
            DontDestroyOnLoad(instanciaCompartida);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene escena, LoadSceneMode modo) {
        Vector3 posicionAparicion = new Vector3(Random.Range(-7f, 2f), 1, Random.Range(-2f, 2f)); // Corregir el '<1>' dependiendo del scenario
        
        if (PhotonNetwork.InRoom) {
            GameObject instanciaPlayer = PhotonNetwork.Instantiate("PlayerOnline",posicionAparicion,Quaternion.identity);
            instanciaPlayer.name="PlayerOnline"+PhotonNetwork.CurrentRoom.PlayerCount.ToString();
            //PhotonNetwork.Instantiate("PlayerOnline", posicionAparicion, Quaternion.identity);
        } else {
            Instantiate(Resources.Load("PlayerOnline"), posicionAparicion, Quaternion.identity);
        }
    }
}
