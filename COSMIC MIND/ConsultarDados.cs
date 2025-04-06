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
       
        textoUsername.text = PlayerPrefs.GetString("username", "Usu�rio Padr�o"); 
        textoUserId.text = PlayerPrefs.GetString("UserID", "ID n�o encontrado"); 
 

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
