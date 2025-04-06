using UnityEngine;

public class AplicarConfiguracao1 : MonoBehaviour
{
    public AudioSource audioMusica;


    private bool musicaAtivada;
   

    void Start()
    {
       
        CarregarConfiguracoes();

       
        AtualizarEstadoMusica();
        
    }

    void CarregarConfiguracoes()
    {
        
        if (PlayerPrefs.HasKey("volumeMusica"))
        {
            audioMusica.volume = PlayerPrefs.GetFloat("volumeMusica");
        }
      

        
        musicaAtivada = PlayerPrefs.GetInt("musicaAtivada", 1) == 1;
       
    }

    void AtualizarEstadoMusica()
    {
      
        audioMusica.mute = !musicaAtivada;
    }

    
}
