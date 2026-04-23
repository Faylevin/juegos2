using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class ManagerDisparo : MonoBehaviour
{
    public Camera playerCamera;
    public Transform origenRayo;


    private float rango = 100.0f;
    private float duracion = 0.1f;
    private float frecuenciaDisparo = 0.25f;
    private float TiempoDisparo;
    private LineRenderer rayoLaser;

    private int danoCausado = 1;
    [SerializeField]private LayerMask LMask;

    private void Awake(){
        rayoLaser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        TiempoDisparo += Time.deltaTime;
        if(Input.GetButtonDown("Fire1") && TiempoDisparo > frecuenciaDisparo){
            Dispara();
        }
    }

    private void Dispara(){
        TiempoDisparo = 0;
        rayoLaser.SetPosition(0,origenRayo.position);
        Vector3 origen = playerCamera.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        if( Physics.Raycast(origen, playerCamera.transform.forward, out hit, rango, LMask )){
            rayoLaser.SetPosition(1,hit.point);
            Destroy(hit.transform.gameObject);


            /*IA_Enemigo enemigo = hit.transform.GetComponent<IA_Enemigo>();
            if(enemigo != null){
                Debug.Log("Colision con enemigo");
                enemigo.TomarDano(danoCausado);
            } else {
                Debug.Log("Nulo");
            } */
        }else{
            rayoLaser.SetPosition(1,origen + (playerCamera.transform.forward) * rango);
        }

        StartCoroutine(DisparaLaser());
    }

        IEnumerator DisparaLaser(){
            rayoLaser.enabled = true;
            yield return new WaitForSeconds(duracion);
            rayoLaser.enabled = false;
        }

}
