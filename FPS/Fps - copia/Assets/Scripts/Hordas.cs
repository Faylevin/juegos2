using System.Collections;
using UnityEngine;
using TMPro;

public class Hordas : MonoBehaviour
{
    public int enemigosVivos;
    public int numRonda;
    public GameObject[] puntosDeSpawn;
    public GameObject prefabEnemigo;

    void Start(){
        numRonda = 0;
    }

    void Update(){
        if(enemigosVivos == 0){
            numRonda++;
            SiguienteOleada(numRonda);
        }
    }

    private void SiguienteOleada(int ronda){
        for (int i = 0; i < ronda; i++){
            int randomPos = Random.Range(0, puntosDeSpawn.Length);
            GameObject puntoEmision = puntosDeSpawn[randomPos];
            GameObject instanciaEnemigo = Instantiate(prefabEnemigo, puntoEmision.transform.position, Quaternion.identity);
            instanciaEnemigo.GetComponent<IA_Enemigo>().hordas = this;
            enemigosVivos++;
        }
    }
    
}