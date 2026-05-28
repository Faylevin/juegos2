using UnityEngine;

public class MusicaManager : MonoBehaviour
{
    public AudioSource musicaMundo1;
    public AudioSource musicaMundo2;
    public AudioSource musicaMundo3;
    public AudioSource sonidoPortal;

    public void CambiarAMundo(int mundo)
    {
      
        musicaMundo1.Stop();
        musicaMundo2.Stop();
        musicaMundo3.Stop();

      
        switch (mundo)
        {
            case 1: musicaMundo1.Play(); break;
            case 2: musicaMundo2.Play(); break;
            case 3: musicaMundo3.Play(); break;
        }
    }

    public void SonidoTeletransporte()
    {
        sonidoPortal.PlayOneShot(sonidoPortal.clip);
    }
}