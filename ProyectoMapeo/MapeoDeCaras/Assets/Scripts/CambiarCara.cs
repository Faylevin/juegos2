using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class CambiarMascara : MonoBehaviour
{
    public GameObject[] facePrefabs;
    public string[] nombresMascaras;
    public AudioClip[] sonidos;
    public TextMeshProUGUI textoMascara;
    private ARFaceManager faceManager;
    private AudioSource audioSource;
    private int index = 0;

    void Start()
    {
        faceManager = GetComponent<ARFaceManager>();
        audioSource = GetComponent<AudioSource>();
        faceManager.facePrefab = facePrefabs[index];
        ActualizarUI();
    }

    public void Siguiente()
    {
        index = (index + 1) % facePrefabs.Length;
        CambiarA(index);
    }

    public void Anterior()
    {
        index = (index - 1 + facePrefabs.Length) % facePrefabs.Length;
        CambiarA(index);
    }

    void CambiarA(int i)
    {
        faceManager.facePrefab = facePrefabs[i];
        StartCoroutine(RecargarFaceManager());
        ActualizarUI();
    }

    IEnumerator RecargarFaceManager()
    {
        foreach (var face in faceManager.trackables)
        {
            Destroy(face.gameObject);
        }

        faceManager.enabled = false;
        yield return new WaitForSeconds(0.2f);
        faceManager.enabled = true;
    }

    void ActualizarUI()
    {
        if (textoMascara != null && nombresMascaras.Length > index)
            textoMascara.text = nombresMascaras[index];

        if (audioSource != null && sonidos.Length > index && sonidos[index] != null)
        {
            audioSource.clip = sonidos[index];
            audioSource.Play();
        }
    }
}