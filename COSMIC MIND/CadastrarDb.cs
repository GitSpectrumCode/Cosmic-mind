using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEngine.SceneManagement;

public class CadastrarDb : MonoBehaviour
{
    [SerializeField] InputField codigoValidar;
    [SerializeField] Text mensagemStatus;
    [SerializeField] Button btnInicio;
    [SerializeField] Button btnVoltar;
    [SerializeField] Button btnReenviarCodigo;
    [SerializeField] EnviarEmail enviarEmail;

    public string database_url = "https://fire-2d917-default-rtdb.firebaseio.com/";
    Usuario dados = new Usuario();

    void Start()
    {
        btnVoltar.onClick.AddListener(VoltarAoMenu);
        btnReenviarCodigo.onClick.AddListener(ReenviarCodigo);
        mensagemStatus.gameObject.SetActive(false);
        GerarUserId();
    }

    private void GerarUserId()
    {
        string userId = "User_" + Random.Range(1000, 9999);
        PlayerPrefs.SetString("UserID", userId);
        Debug.Log("ID do usuário gerado: " + userId);
    }

    public void savedata_toFirebase()
    {
        VerificarCodigo();
    }

    public void VerificarCodigo()
    {
        btnInicio.interactable = false;

        string codigoSalvo = PlayerPrefs.GetString("ValidationCode");

        Debug.Log("Código salvo: " + codigoSalvo);
        Debug.Log("Código inserido: " + codigoValidar.text);

        if (codigoSalvo == codigoValidar.text)
        {
            ExibirMensagem("Código correto! Usuário validado com sucesso.", Color.green);
            SalvarDadosNoFirebase();
        }
        else
        {
            ExibirMensagem("Código incorreto. Tente novamente.", Color.red);
            btnInicio.interactable = true;
        }
    }

    public void SalvarDadosNoFirebase()
    {
        dados.Username = PlayerPrefs.GetString("username");
        dados.Email = PlayerPrefs.GetString("email");
        dados.Senha = PlayerPrefs.GetString("senha");
        dados.Salt = PlayerPrefs.GetString("salt");
        dados.CodigoValidacao = codigoValidar.text;
        dados.UserId = PlayerPrefs.GetString("UserID");

        // Formata o e-mail para ser usado no Firebase
        string emailFormatado = dados.Email.Replace(".", "_").Replace("@", "_");

        btnInicio.interactable = false;

        // Envia os dados para o Firebase
        RestClient.Post(database_url + "/" + emailFormatado + ".json", dados)
            .Then(response =>
            {
                ExibirMensagem("Dados registrados com sucesso!", Color.green);
                // LimparPlayerPrefs();  // Não vamos limpar os dados do PlayerPrefs
                Invoke("CarregarTelaMenu", 2f);
            })
            .Catch(error =>
            {
                Debug.LogError("Erro ao enviar dados: " + error);
                ExibirMensagem("Erro ao registrar dados. Tente novamente.", Color.red);
            })
            .Finally(() =>
            {
                btnInicio.interactable = true;
            });
    }

    public void ReenviarCodigo()
    {
        btnReenviarCodigo.interactable = false;

        string codigoNovo = GerarCodigoAlfanumerico(6);

        PlayerPrefs.SetString("ValidationCode", codigoNovo);

        if (enviarEmail != null)
        {
            enviarEmail.EnviarCodigoValidacao(PlayerPrefs.GetString("email"), codigoNovo);
            ExibirMensagem("Novo código de 6 dígitos enviado ao seu e-mail.", Color.yellow);
        }
        else
        {
            Debug.LogError("O componente EnviarEmail não foi encontrado.");
            ExibirMensagem("Erro ao reenviar código. Componente de e-mail não encontrado.", Color.red);
        }

        btnReenviarCodigo.interactable = true;
    }

    private string GerarCodigoAlfanumerico(int tamanho)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        System.Text.StringBuilder result = new System.Text.StringBuilder(tamanho);
        System.Random random = new System.Random();

        // Gera um código aleatório de letras e números
        for (int i = 0; i < tamanho; i++)
        {
            result.Append(chars[random.Next(chars.Length)]);
        }

        return result.ToString();
    }

    private void VoltarAoMenu()
    {
        SceneManager.LoadScene("TelaCadastrar");
    }

    private void ExibirMensagem(string texto, Color cor)
    {
        if (!string.IsNullOrEmpty(texto) && mensagemStatus != null)
        {
            mensagemStatus.text = texto;
            mensagemStatus.color = cor;
            mensagemStatus.gameObject.SetActive(true);
        }
        else
        {
            mensagemStatus.text = "";
            mensagemStatus.gameObject.SetActive(false);
        }
    }

    // Removemos o método LimparPlayerPrefs() que apagava os dados
    //private void LimparPlayerPrefs()
    //{
    //    // Remove as informações salvas nos PlayerPrefs após o registro
    //    PlayerPrefs.DeleteKey("username");
    //    PlayerPrefs.DeleteKey("email");
    //    PlayerPrefs.DeleteKey("ValidationCode");
    //    PlayerPrefs.DeleteKey("UserID");
    //    Debug.Log("PlayerPrefs limpo após o registro bem-sucedido.");
    //}

    private void CarregarTelaMenu()
    {
        SceneManager.LoadScene("TelaMenu");
    }
}
