using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PerfilUsuario : MonoBehaviour
{
    [SerializeField] Text textoUsername; 
    [SerializeField] Text textoUserId;    
    [SerializeField] Text textoEmail;    
    [SerializeField] Button botaoAlterar;
    [SerializeField] Button botaoDesempenho; 

    void Start()
    {
       
        textoUsername.text = PlayerPrefs.GetString("username", "Usuário Padrão"); 
        textoUserId.text = PlayerPrefs.GetString("UserID", "ID não encontrado"); 
 

        botaoAlterar.onClick.AddListener(VaiParaAlterarPerfil);
        botaoDesempenho.onClick.AddListener(VaiParaTelaDesempenho);
    }

    
    private void VaiParaAlterarPerfil()
    {
        SceneManager.LoadScene("TelaEditarPerfil"); 
    }

    
    private void VaiParaTelaDesempenho()
    {
        SceneManager.LoadScene("TelaDesempenho");
    }
}
