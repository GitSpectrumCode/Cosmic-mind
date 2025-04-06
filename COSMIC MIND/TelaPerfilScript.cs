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
       
        string username = PlayerPrefs.GetString("NomeUsuario", "Usuário Padrão");
        int avatarIndex = PlayerPrefs.GetInt("AvatarSelecionadoIndex", 0);

       
        textoUsername.text = username;

    
        if (avatares != null && avatares.Length > avatarIndex)
        {
            imagePerfil.sprite = avatares[avatarIndex];  
        }
        else
        {
            
            Debug.LogWarning("Avatar não encontrado! Definindo avatar padrão.");
            if (avatares != null && avatares.Length > 0)
            {
                imagePerfil.sprite = avatares[0]; 
            }
        }
    }
}
