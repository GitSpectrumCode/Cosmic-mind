using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TelaPerfilScript : MonoBehaviour
{
    [SerializeField] Text textoUsername;  
    [SerializeField] Image imagePerfil;   
    [SerializeField] Sprite[] avatares;   

    void Start()
    {
        
        CarregarDadosPerfil();
    }

    public void CarregarDadosPerfil()
    {
       
        string username = PlayerPrefs.GetString("NomeUsuario", "Usu�rio Padr�o");
        int avatarIndex = PlayerPrefs.GetInt("AvatarSelecionadoIndex", 0);

       
        textoUsername.text = username;

    
        if (avatares != null && avatares.Length > avatarIndex)
        {
            imagePerfil.sprite = avatares[avatarIndex];  
        }
        else
        {
            
            Debug.LogWarning("Avatar n�o encontrado! Definindo avatar padr�o.");
            if (avatares != null && avatares.Length > 0)
            {
                imagePerfil.sprite = avatares[0]; 
            }
        }
    }
}
