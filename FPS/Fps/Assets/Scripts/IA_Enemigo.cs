using UnityEngine;
using UnityEngine.AI;

public class IA_Enemigo : MonoBehaviour
{   
    [SerializeField] private NavMeshAgent agente;
    private GameObject player;
    private float velEnemigo;
    private float dist;
    private float frecAtaque = 2.5f, tiempSigAtaque = 0, iniciaConteo;
    public static int vidaEnemigo;

    void Start()
    {
        player = GameObject.Find("Capsule");
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
        vidaEnemigo -= dano;

        if(vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }
}