using UnityEngine;

public class AplicarConfiguracao : MonoBehaviour
{
    public AudioSource audioMusica;
    public AudioSource audioEfeitos;

    private bool musicaAtivada;
    private bool efeitosAtivados;

    void Start()
    {
       
        CarregarConfiguracoes();

       
        AtualizarEstadoMusica();
        AtualizarEstadoEfeitos();
    }

    void CarregarConfiguracoes()
    {
        
        if (PlayerPrefs.HasKey("volumeMusica"))
        {
            audioMusica.volume = PlayerPrefs.GetFloat("volumeMusica");
        }
        if (PlayerPrefs.HasKey("volumeEfeitos"))
        {
            audioEfeitos.volume = PlayerPrefs.GetFloat("volumeEfeitos");
        }

        
        musicaAtivada = PlayerPrefs.GetInt("musicaAtivada", 1) == 1;
        efeitosAtivados = PlayerPrefs.GetInt("efeitosAtivados", 1) == 1;
    }

    void AtualizarEstadoMusica()
    {
      
        audioMusica.mute = !musicaAtivada;
    }

    void AtualizarEstadoEfeitos()
    {

        audioEfeitos.mute = !efeitosAtivados;
    }
}
