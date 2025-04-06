using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System;
using UnityEngine.SceneManagement;

public class DadosCadastro : MonoBehaviour
{
    [SerializeField] InputField username;
    [SerializeField] InputField email;
    [SerializeField] InputField senha1;
    [SerializeField] InputField senhaConfirma;

    [SerializeField] Text mensagem;
    [SerializeField] Button proximaTelaButton;
    [SerializeField] Button voltarButton;
    [SerializeField] Button olhosenha;
    [SerializeField] Button olhosenha2;
    [SerializeField] Sprite olhoAberto;
    [SerializeField] Sprite olhoFechado;

    private bool senhaVisivel1 = false;
    private bool senhaVisivel2 = false;

    Usuario dados = new Usuario();
    EnviarEmail emailService = new EnviarEmail();

    void Start()
    {
        mensagem.gameObject.SetActive(false);
        voltarButton.onClick.AddListener(VoltarAoMenu);
        olhosenha.onClick.AddListener(ToggleSenhaVisivel1);
        olhosenha2.onClick.AddListener(ToggleSenhaVisivel2);
    }

    void ToggleSenhaVisivel1()
    {
        senhaVisivel1 = !senhaVisivel1;
        senha1.contentType = senhaVisivel1 ? InputField.ContentType.Standard : InputField.ContentType.Password;
        senha1.ForceLabelUpdate();

        olhosenha.image.sprite = senhaVisivel1 ? olhoAberto : olhoFechado;
    }

    void ToggleSenhaVisivel2()
    {
        senhaVisivel2 = !senhaVisivel2;
        senhaConfirma.contentType = senhaVisivel2 ? InputField.ContentType.Standard : InputField.ContentType.Password;
        senhaConfirma.ForceLabelUpdate();

        olhosenha2.image.sprite = senhaVisivel2 ? olhoAberto : olhoFechado;
    }

    public static string HashSenhaComSalt(string senha, string salt)
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

    public void GuardarDados()
    {
        mensagem.gameObject.SetActive(true);
        dados.Username = username.text;
        dados.Email = email.text;

        if (!ValidarEmail(email.text))
        {
            ExibirMensagem("E-mail inválido. Insira um e-mail válido.", Color.red);
            return;
        }

        if (senha1.text.Length != 6)
        {
            ExibirMensagem("A senha deve ter exatamente 6 caracteres.", Color.red);
            return;
        }

        if (senha1.text != senhaConfirma.text)
        {
            ExibirMensagem("As senhas não coincidem. Verifique e tente novamente.", Color.red);
            return;
        }

        string salt = GerarSalt();
        string senhaHash = HashSenhaComSalt(senha1.text, salt);

        if (!string.IsNullOrEmpty(username.text) &&
            !string.IsNullOrEmpty(email.text) &&
            !string.IsNullOrEmpty(senhaHash))
        {
            PlayerPrefs.SetString("username", username.text);
            PlayerPrefs.SetString("email", email.text);
            PlayerPrefs.SetString("senha", senhaHash);
            PlayerPrefs.SetString("salt", salt);
            PlayerPrefs.Save();

            ExibirMensagem("Dados salvos com sucesso!", Color.green);

            string codigoValidacao = GerarCodigo.GenerateCode(6);
            PlayerPrefs.SetString("ValidationCode", codigoValidacao);
            PlayerPrefs.Save();

            StartCoroutine(EnviarCodigoEmail(email.text, codigoValidacao));
        }
        else
        {
            ExibirMensagem("Por favor, preencha todos os campos.", Color.red);
        }
    }

    public string GerarSalt()
    {
        byte[] saltBytes = new byte[16];
        using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    bool ValidarEmail(string email)
    {
        string padraoEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, padraoEmail);
    }

    void ExibirMensagem(string texto, Color cor)
    {
        mensagem.text = texto;
        mensagem.color = cor;
        StartCoroutine(RemoverMensagem());
    }

    IEnumerator RemoverMensagem()
    {
        yield return new WaitForSeconds(3);
        mensagem.text = "";
    }

    IEnumerator EnviarCodigoEmail(string destinatario, string codigoValidacao)
    {
        // Desabilita o botão até que o envio do e-mail seja concluído
        proximaTelaButton.interactable = false;
        ExibirMensagem("Enviando código de validação, por favor aguarde...", Color.yellow);
        emailService.EnviarCodigoValidacao(destinatario, codigoValidacao);
        yield return new WaitForSeconds(3);

        bool emailEnviado = VerificarEnvioEmail();

        if (emailEnviado)
        {
            ExibirMensagem("E-mail enviado com sucesso!", Color.green);
            proximaTelaButton.interactable = true; // Reabilita o botão após o envio
            SceneManager.LoadScene("TelaValidar");
        }
        else
        {
            ExibirMensagem("Erro ao enviar e-mail. Tente novamente.", Color.red);
            proximaTelaButton.interactable = true; // Reabilita o botão mesmo em caso de erro
        }
    }

    bool VerificarEnvioEmail()
    {
        return true;
    }

    void VoltarAoMenu()
    {
        SceneManager.LoadScene("TelaInicio");
    }
}
