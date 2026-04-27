using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class ManagerDisparo : MonoBehaviour
{
    public Camera playerCamera;
    public Transform origenRayo;

    public GameObject objPistola, objRifle, objEscopeta;

    public Arma pistola;
    public Arma rifle;
    public Arma escopeta;

    [SerializeField] private float rango;
    [SerializeField] private float duracion;
    [SerializeField] private float frecuenciaDisparo;

    [SerializeField] private int danoCausado;
    [SerializeField] private int capacidadArma;

    private float TiempoDisparo;

    [SerializeField] private LayerMask LMask, otro;
    [SerializeField] private ParticleSystem particulasDisparo;
    public GameObject impacto;

    public static int puntosPlayer;

    // 🔥 ANIMACIÓN CAMBIO ARMA
    [SerializeField] private float velocidadCambio = 8f;
    [SerializeField] private float bajada = 0.5f;

    private bool cambiando = false;
    private Vector3 posInicial;
    private GameObject armaActual;

    private void Awake()
    {
        pistola = new Arma(30, 1.0f, 1, 5);
        rifle = new Arma(50, 0.25f, 2, 10);
        escopeta = new Arma(77, 1.5f, 5, 1);

        OcultaArmas();
        objPistola.SetActive(true);

        armaActual = objPistola;
        posInicial = transform.localPosition;

        rango = pistola.alcance;
        frecuenciaDisparo = pistola.frecuenciaDisparo;
        danoCausado = pistola.danoCausado;
        capacidadArma = pistola.capacidad;

        puntosPlayer = 0;
    }

    void Update()
    {
        CambiaArma();

        TiempoDisparo += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && TiempoDisparo > frecuenciaDisparo && !cambiando)
        {
            Dispara();
        }
    }

    private void CambiaArma()
    {
        if (cambiando) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            StartCoroutine(CambiarArmaAnim(objPistola, pistola));

        if (Input.GetKeyDown(KeyCode.Alpha2))
            StartCoroutine(CambiarArmaAnim(objRifle, rifle));

        if (Input.GetKeyDown(KeyCode.Alpha3))
            StartCoroutine(CambiarArmaAnim(objEscopeta, escopeta));
    }

    IEnumerator CambiarArmaAnim(GameObject nuevaArma, Arma datos)
    {
        cambiando = true;

        // 🔻 BAJAR
        Vector3 abajo = posInicial + Vector3.down * bajada;

        while (Vector3.Distance(transform.localPosition, abajo) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                abajo,
                Time.deltaTime * velocidadCambio
            );
            yield return null;
        }

        // 🔄 CAMBIAR
        armaActual.SetActive(false);
        nuevaArma.SetActive(true);
        armaActual = nuevaArma;

        rango = datos.alcance;
        frecuenciaDisparo = datos.frecuenciaDisparo;
        danoCausado = datos.danoCausado;
        capacidadArma = datos.capacidad;

        // 🔺 SUBIR
        while (Vector3.Distance(transform.localPosition, posInicial) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                posInicial,
                Time.deltaTime * velocidadCambio
            );
            yield return null;
        }

        transform.localPosition = posInicial;
        cambiando = false;
    }

    private void Dispara()
    {
        if (particulasDisparo != null)
            particulasDisparo.Play();

        TiempoDisparo = 0;

        Vector3 origen = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(origen, playerCamera.transform.forward, out hit, rango, LMask))
        {
            if (impacto != null)
            {
                GameObject objImpacto = Instantiate(impacto, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(objImpacto, 1.2f);
            }

            IA_Enemigo enemigo = hit.transform.GetComponent<IA_Enemigo>();

            if (enemigo != null)
            {
                puntosPlayer++;
                enemigo.TomarDano(danoCausado);
            }

            Destroy(hit.transform.gameObject);
        }
        else if (Physics.Raycast(origen, playerCamera.transform.forward, out hit, rango, otro))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * 70.0f);
            }

            if (impacto != null)
            {
                GameObject objImpacto = Instantiate(impacto, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(objImpacto, 1.2f);
            }
        }
    }

    private void OcultaArmas()
    {
        objPistola.SetActive(false);
        objRifle.SetActive(false);
        objEscopeta.SetActive(false);
    }
}