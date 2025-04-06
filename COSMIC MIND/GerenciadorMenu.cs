using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GerenciadorMenu : MonoBehaviour
{
    [SerializeField] Button btnJogar;
    [SerializeField] Button btnPerfil;
    [SerializeField] Button btnOpcoes;
    [SerializeField] Button btnSuporte;
    [SerializeField] Button btnTutoriais;

    void Start()
    {
        btnJogar.onClick.AddListener(IrParaTelaNarracao);
        btnPerfil.onClick.AddListener(IrParaTelaPerfil);
        btnOpcoes.onClick.AddListener(IrParaTelaConfiguracao);
        btnSuporte.onClick.AddListener(IrParaTelaSuporte);
        btnTutoriais.onClick.AddListener(IrParaMenuTutorial);
    }

    void IrParaTelaNarracao()
    {
        SceneManager.LoadScene("TelaNarracao 1");
    }

    void IrParaTelaPerfil()
    {
        SceneManager.LoadScene("TelaPerfil");
    }

    void IrParaTelaConfiguracao()
    {
        SceneManager.LoadScene("TelaConfiguracao");
    }

    void IrParaTelaSuporte()
    {
        SceneManager.LoadScene("TelaSuporte");
    }

    void IrParaMenuTutorial()
    {
        SceneManager.LoadScene("TutoriaisTelaInicial");
    }
}
