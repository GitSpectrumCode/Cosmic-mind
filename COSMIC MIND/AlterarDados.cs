using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class AlterarAvatar : MonoBehaviour
{
    [SerializeField] private Image imagemPerfil;  
    [SerializeField] private Button botaoCamera;  
    [SerializeField] private Button botaoVoltar;  
    [SerializeField] private Button botaoSalvar; 
    [SerializeField] private Text mensagemStatus; 
    [SerializeField] private Text mensagemInformativa; 
    [SerializeField] private InputField inputNome;  

    
    [SerializeField] private Sprite[] avatares;  

    private bool dadosAlterados = false; 

    private void Start()
    {
        
        CarregarAvatar();

       
        botaoCamera.onClick.AddListener(TrocarAvatar);
        botaoSalvar.onClick.AddListener(SalvarDados);
        botaoVoltar.onClick.AddListener(VoltarParaTelaPerfil);

       
        ExibirMensagemInformativa();

        
        StartCoroutine(EsconderMensagemInformativa());
    }

    private void CarregarAvatar()
    {
     
        string nomeSalvo = PlayerPrefs.GetString("username", "");
        int avatarIndexSalvo = PlayerPrefs.GetInt("AvatarSelecionadoIndex", 0); 

       
        inputNome.text = nomeSalvo;

        
        if (avatares != null && avatares.Length > avatarIndexSalvo)
        {
            imagemPerfil.sprite = avatares[avatarIndexSalvo]; 
        }
    }

    private void TrocarAvatar()
    {
        
        int avatarIndex = Random.Range(0, avatares.Length); 

       
        if (avatares != null && avatares.Length > avatarIndex)
        {
            imagemPerfil.sprite = avatares[avatarIndex];

           
            dadosAlterados = true;

            ExibirMensagem("Avatar alterado com sucesso!", Color.green);
        }
        else
        {
            ExibirMensagem("Erro ao carregar o avatar!", Color.red);
        }
    }

    private void SalvarDados()
    {
      
        string nomeNovo = inputNome.text;

        
        if (string.IsNullOrEmpty(nomeNovo))
        {
            ExibirMensagem("Erro: O nome não pode estar vazio!", Color.red);
            return;
        }


        PlayerPrefs.SetString("NomeUsuario", nomeNovo);

       
        int avatarIndexSelecionado = System.Array.IndexOf(avatares, imagemPerfil.sprite); 
        PlayerPrefs.SetInt("AvatarSelecionadoIndex", avatarIndexSelecionado);

     
        PlayerPrefs.Save();

      
        dadosAlterados = false;

       
        ExibirMensagem("Dados atualizados com sucesso!", Color.green);

      
        AtualizarTelaPerfil();
    }

    private void VoltarParaTelaPerfil()
    {
       
        if (dadosAlterados)
        {
            ExibirMensagem("Os dados não foram salvos! Clique em 'Salvar' para salvar as alterações.", Color.red);
        }
        else
        {
           
            SceneManager.LoadScene("TelaPerfil");
        }
    }

    private void ExibirMensagem(string mensagem, Color cor)
    {
        mensagemStatus.text = mensagem;
        mensagemStatus.color = cor;
    }

    private void ExibirMensagemInformativa()
    {
      
        mensagemInformativa.text = "Clique no ícone da galeria para mudar seu avatar!";
        mensagemInformativa.color = Color.yellow;  
    }

    private IEnumerator EsconderMensagemInformativa()
    {
       
        yield return new WaitForSeconds(5f);
        mensagemInformativa.text = "";
    }

    private void AtualizarTelaPerfil()
    {
      
        SceneManager.LoadScene("TelaPerfil");
    }
}
