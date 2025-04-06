using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Proyecto26;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public class LogarUsuario : MonoBehaviour
{
    [SerializeField] private InputField email;
    [SerializeField] private InputField senha;
    [SerializeField] private Text mensagemStatus;
    [SerializeField] private Button botaoLogin;
    [SerializeField] private Button botaoOlho;
    [SerializeField] private Button botaoVoltar;
    [SerializeField] private Button botaoRedefinirSenha;

    [SerializeField] private Sprite olhoAbertoSprite;  
    [SerializeField] private Sprite olhoFechadoSprite; 

    private bool senhaVisivel = false;

    public string database_url = "https://fire-2d917-default-rtdb.firebaseio.com/";
    Usuario user = new Usuario();

    private void Start()
    {
        botaoOlho.onClick.AddListener(TrocarVisibilidadeSenha);
        botaoVoltar.onClick.AddListener(VoltarParaTelaInicio);
        botaoRedefinirSenha.onClick.AddListener(RedefinirSenha);

        mensagemStatus.gameObject.SetActive(false);
    }

    public void loginUser()
    {
        botaoLogin.interactable = false;

        if (!ValidarEmail(email.text))
        {
            ExibirMensagem("Formato de e-mail inválido.", Color.red);
            botaoLogin.interactable = true;
            return;
        }

        if (!ValidarSenha(senha.text))
        {
            ExibirMensagem("A senha deve ter exatamente 6 dígitos.", Color.red);
            botaoLogin.interactable = true;
            return;
        }

        string emailSalvo = PlayerPrefs.GetString("email", "");
        string senhaSalvaHash = PlayerPrefs.GetString("senha", "");
        //Substitua o email dudabelles... pelo o seu email
        string emailSegundoSalvo = PlayerPrefs.GetString("email2", "luigivalesiborgesdeoliveira8@gmal.com");
        string senhaSegundoSalvo = PlayerPrefs.GetString("senha2", "141223");

        string emailTerceiroSalvo = PlayerPrefs.GetString("email3", "paulo@gmail.com");
        string senhaTerceiroSalvo = PlayerPrefs.GetString("senha3", "444444");

        string salt = PlayerPrefs.GetString("salt", "");
        string senhaInseridaHash = HashSenhaComSalt(senha.text, salt);

        if ((email.text == emailSalvo && senhaInseridaHash == senhaSalvaHash) ||
            (email.text == emailSegundoSalvo && senha.text == senhaSegundoSalvo) ||
            (email.text == emailTerceiroSalvo && senha.text == senhaTerceiroSalvo))
        {
            ExibirMensagem("Login realizado com sucesso!", Color.green);

            if (email.text == "paulo@gmail.com" && senha.text == "444444")
            {
                Invoke("CarregarTelaMenu2", 3f);
            }
            else
            {
                Invoke("CarregarTelaMenu", 3f);
            }
        }
        else
        {
            ExibirMensagem("Usuário não encontrado.", Color.red);
            botaoLogin.interactable = true;
        }
    }

    private void TrocarVisibilidadeSenha()
    {
        senhaVisivel = !senhaVisivel;
        senha.contentType = senhaVisivel ? InputField.ContentType.Standard : InputField.ContentType.Password;

       
        Sprite spriteAtual = senhaVisivel ? olhoAbertoSprite : olhoFechadoSprite;
        botaoOlho.GetComponentInChildren<Image>().sprite = spriteAtual;

        senha.ForceLabelUpdate();
    }

    private void VoltarParaTelaInicio()
    {
        SceneManager.LoadScene("TelaInicio");
    }

    public void RedefinirSenha()
    {
        SceneManager.LoadScene("VerificarId");
    }

    private bool ValidarEmail(string email)
    {
        string padraoEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, padraoEmail);
    }

    private bool ValidarSenha(string senha)
    {
        return senha.Length == 6;
    }

    private void ExibirMensagem(string texto, Color cor)
    {
        if (mensagemStatus != null)
        {
            mensagemStatus.text = texto;
            mensagemStatus.color = cor;
            mensagemStatus.gameObject.SetActive(true);
            Invoke("LimparMensagem", 3f);
        }
    }

    private void LimparMensagem()
    {
        if (mensagemStatus != null)
        {
            mensagemStatus.text = "";
            mensagemStatus.gameObject.SetActive(false);
        }
    }

    private void CarregarTelaMenu()
    {
        SceneManager.LoadScene("TelaMenu");
    }

    private void CarregarTelaMenu2()
    {
        SceneManager.LoadScene("TelaMenu 2");
    }

    private string HashSenhaComSalt(string senha, string salt)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            string senhaComSalt = senha + salt;
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senhaComSalt));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
