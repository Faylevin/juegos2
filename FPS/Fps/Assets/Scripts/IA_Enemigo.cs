using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;
public class IA_Enemigo : MonoBehaviour
{   
    [SerializeField] private NavMeshAgent agente;
    private GameObject player;
    private float velEnemigo;
    private float dist;
    private float frecAtaque = 2.5f, tiempSigAtaque = 0, iniciaConteo;
    public int vidaEnemigo; // ← quitado static
    public Hordas hordas;
    public PhotonView pvEnemigo;
	
    void Start()
    {
        player = GameObject.Find("Capsule");
        hordas = GameObject.Find("Hordas").GetComponent<Hordas>();
        agente = GetComponent<NavMeshAgent>();
        dist = Vector3.Distance(player.transform.position, transform.position);
        agente.speed = Random.Range(1.0f, 5.0f);
        vidaEnemigo = 1;
    }

    void Update()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);
        if(tiempSigAtaque > 0)
        {
            tiempSigAtaque = frecAtaque + iniciaConteo - Time.time;
        }
        else
        {
            tiempSigAtaque = 0;
            agente.SetDestination(player.transform.position);
            VidasPlayer.puedePerderVida = 1;
        }
    }

    private void OnTriggerEnter(Collider obj)
    {
        if(obj.tag == "Player")
        {
            tiempSigAtaque = frecAtaque;
            iniciaConteo = Time.time;
            obj.transform.GetComponentInChildren<VidasPlayer>().TomarDano(1);
        }
    }

    public void TomarDano(int dano)
    {
        if (PhotonNetwork.InRoom)
        {
            pvEnemigo.RPC("AplicarDemo", RpcTarget.All, dano, pvEnemigo.ViewID);
        }
        else
        {
            AplicarDemo(dano, 0);
        }
    }

    [PunRPC]
    public void AplicarDemo(int dano, int viewID)
    {
        if (!PhotonNetwork.InRoom || pvEnemigo.ViewID == viewID)
        {
            vidaEnemigo -= dano;
            if (vidaEnemigo <= 0)
            {
                if (!PhotonNetwork.InRoom)
                {
                    hordas.enemigosVivos--;
                    Destroy(gameObject);
                }
                else if (PhotonNetwork.IsMasterClient && pvEnemigo.IsMine)
                {
                    hordas.enemigosVivos--;
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }
    }
}