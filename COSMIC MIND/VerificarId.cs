using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using UnityEngine.SceneManagement;

public class VerificarIdPorEmail : MonoBehaviour
{
    [SerializeField] private InputField inputId;
    [SerializeField] private Text mensagemStatus;
    [SerializeField] private Button botaoVerificar;
    [SerializeField] private Button botaoVoltar;

    public string database_url = "https://fire-2d917-default-rtdb.firebaseio.com/";

    private void Start()
    {
        botaoVerificar.onClick.AddListener(VerificarIdUsuario);
        mensagemStatus.gameObject.SetActive(false);

        botaoVoltar.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("TelaLogin");
        });
    }

    public void VerificarIdUsuario()
    {
        mensagemStatus.gameObject.SetActive(false);

        string idDigitado = inputId.text;
        if (string.IsNullOrEmpty(idDigitado))
        {
            ExibirMensagem("Por favor, insira o ID.", Color.red);
            return;
        }

        VerificarIdNoFirebase(idDigitado);
    }

    private void VerificarIdNoFirebase(string idDigitado)
    {
        Usuario usuarioSimulado = new Usuario
        {
            UserId = "User_4465",
        };

        Debug.Log("Resposta simulada do Firebase: " + usuarioSimulado.UserId);

        if (usuarioSimulado != null && usuarioSimulado.UserId == idDigitado)
        {
            ExibirMensagem("ID verificado com sucesso!", Color.green);

            SceneManager.LoadScene("TelaRedefinirSenha");
        }
        else
        {
            ExibirMensagem("ID inválido!", Color.red);
        }
    }

    private void ExibirMensagem(string texto, Color cor)
    {
        mensagemStatus.text = texto;
        mensagemStatus.color = cor;
        mensagemStatus.gameObject.SetActive(true);
        Invoke("LimparMensagem", 3f);
    }

    private void LimparMensagem()
    {
        mensagemStatus.text = "";
        mensagemStatus.gameObject.SetActive(false);
    }
}
