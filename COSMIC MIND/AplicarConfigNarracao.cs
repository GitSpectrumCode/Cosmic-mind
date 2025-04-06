using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AplicarConfigNarracao : MonoBehaviour
{
    public AudioSource musica;
    public AudioSource efeitoSom;
    public Text legendaTexto;

    void Start()
    {
        // Carregar configurações salvas
        int tamanhoLegenda = Mathf.Clamp(PlayerPrefs.GetInt("TamanhoLegenda", 40), 40, 70);
        bool estadoMusica = PlayerPrefs.GetInt("MusicaAtivada", 1) == 1;
        bool estadoEfeitoSom = PlayerPrefs.GetInt("EfeitoSomAtivado", 1) == 1;

        // Aplicar configurações
        if (musica != null)
        {
            musica.enabled = estadoMusica;
        }

        if (efeitoSom != null)
        {
            efeitoSom.enabled = estadoEfeitoSom;
        }

        if (legendaTexto != null)
        {
            legendaTexto.fontSize = tamanhoLegenda;
        }
    }

    void VoltarParaMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
