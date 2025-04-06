using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class PedirSuporte : MonoBehaviour
{
    [SerializeField] Button btnEmail;
    [SerializeField] Button btnVoltar;

    void Start()
    {
        btnEmail.onClick.AddListener(EnviarEmail);
        btnVoltar.onClick.AddListener(VolverTelaAnterior);
    }

    void EnviarEmail()
    {
        string emailDestino = "profissionalspectrum@gmail.com"; 
        string assunto = "Feedback ou Relato de Bug - Cosmic Mind";
        string corpoEmail = "Descreva seu feedback ou o bug encontrado aqui...";

       
        string url = $"mailto:{emailDestino}?subject={UnityWebRequest.EscapeURL(assunto)}&body={UnityWebRequest.EscapeURL(corpoEmail)}";
        Application.OpenURL(url);
    }

    void VolverTelaAnterior()
    {
        SceneManager.LoadScene("TelaMenu"); 
    }
}
