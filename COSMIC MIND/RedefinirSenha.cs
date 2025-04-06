using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Text.RegularExpressions;

public class RedefinirSenha : MonoBehaviour
{
    [SerializeField] private InputField senhaNova;
    [SerializeField] private InputField confirmarSenha;
    [SerializeField] private Text mensagemStatus;
    [SerializeField] private Button botaoAtualizarSenha;
    [SerializeField] private Button botaoOlhoSenhaNova;
    [SerializeField] private Button botaoOlhoConfirmarSenha;
    [SerializeField] private Sprite olhoAbertoSprite;
    [SerializeField] private Sprite olhoFechadoSprite;

    private bool senhaNovaVisivel = false;
    private bool confirmarSenhaVisivel = false;

    private void Start()
    {
        botaoOlhoSenhaNova.onClick.AddListener(TrocarVisibilidadeSenhaNova);
        botaoOlhoConfirmarSenha.onClick.AddListener(TrocarVisibilidadeConfirmarSenha);
        mensagemStatus.gameObject.SetActive(false);
        botaoAtualizarSenha.interactable = true;
    }

    public void AtualizarSenha()
    {
        botaoAtualizarSenha.interactable = false;

        if (!ValidarSenha(senhaNova.text))
        {
            ExibirMensagem("A senha deve ter exatamente 6 dígitos.", Color.red);
            botaoAtualizarSenha.interactable = true;
            return;
        }

        if (senhaNova.text != confirmarSenha.text)
        {
            ExibirMensagem("As senhas não coincidem. Tente novamente.", Color.red);
            botaoAtualizarSenha.interactable = true;
            return;
        }

        // Define a nova senha para um valor padrão
        string novaSenhaPadrao = "141223";

        
        ExibirMensagem("Senha redefinida com sucesso!", Color.green);

      
        Invoke("CarregarTelaLogin", 3f);
    }

    private void TrocarVisibilidadeSenhaNova()
    {
        senhaNovaVisivel = !senhaNovaVisivel;
        senhaNova.contentType = senhaNovaVisivel ? InputField.ContentType.Standard : InputField.ContentType.Password;
        botaoOlhoSenhaNova.GetComponent<Image>().sprite = senhaNovaVisivel ? olhoAbertoSprite : olhoFechadoSprite;
        senhaNova.ForceLabelUpdate();
    }

    private void TrocarVisibilidadeConfirmarSenha()
    {
        confirmarSenhaVisivel = !confirmarSenhaVisivel;
        confirmarSenha.contentType = confirmarSenhaVisivel ? InputField.ContentType.Standard : InputField.ContentType.Password;
        botaoOlhoConfirmarSenha.GetComponent<Image>().sprite = confirmarSenhaVisivel ? olhoAbertoSprite : olhoFechadoSprite;
        confirmarSenha.ForceLabelUpdate();
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

    private void CarregarTelaLogin()
    {
        SceneManager.LoadScene("TelaLogin");
    }
}
