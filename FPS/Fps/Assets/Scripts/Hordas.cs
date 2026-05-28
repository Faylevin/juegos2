using System.Collections;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Hordas : MonoBehaviour
{
    public int enemigosVivos;
    public int numRonda;
    public GameObject[] puntosDeSpawn;
    //public GameObject prefabEnemigo;
    public PhotonView pv;

    void Start(){
        numRonda = 0;
    }

      void Update(){
		if(!PhotonNetwork.InRoom || (PhotonNetwork.IsMasterClient && pv.IsMine)){
			if (enemigosVivos == 0){
				numRonda++;
				SiguienteOleada(numRonda);
			}
		}
    }

  private void SiguienteOleada(int ronda){
		for (int i = 0; i < ronda * 5; i++){
			int randomPos = Random.Range(0, puntosDeSpawn.Length);
			GameObject puntoEmision = puntosDeSpawn[randomPos];
			GameObject instanciaEnemigo;
			if (PhotonNetwork.InRoom)//Nesesitamos el enemigo en Resources para hacer la instanciacion en linea
			{
				instanciaEnemigo = PhotonNetwork.Instantiate("Enemigo",puntoEmision.transform.position,Quaternion.identity);
			}
			else
			{
				instanciaEnemigo = Instantiate(Resources.Load("Enemigo"),puntoEmision.transform.position,Quaternion.identity) as GameObject;
			}
			instanciaEnemigo.GetComponent<IA_Enemigo>().hordas = GetComponent<Hordas>();
			enemigosVivos++;
		}
	}
    
}