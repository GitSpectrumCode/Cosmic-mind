using System;
using System.Net;
using System.Net.Mail;
using UnityEngine;

public class EnviarEmail : MonoBehaviour
{
    
        [Header("SMTP Settings")]
        [SerializeField]
        private string emailRemetente = "profissionalspectrum@gmail.com";
        [SerializeField]
        private string senhaRemetente = "vdab yfpb cyrl obtz";
        [SerializeField]
        private string smtpServidor = "smtp.gmail.com";
        [SerializeField]
        private int smtpPorta = 587;

        private string ultimoCodigoValidacao;

        public void EnviarCodigoValidacao(string destinatario, string codigoValidacao)
        {
            ultimoCodigoValidacao = codigoValidacao;

            // Salvar o código de validação no PlayerPrefs
            PlayerPrefs.SetString("codigoSalvo", codigoValidacao.Trim());
            PlayerPrefs.Save();

            Debug.Log($"[EmailValidar] Código de validação '{codigoValidacao}' salvo no PlayerPrefs.");

            string urlImagem = "https://github.com/heeybelles/ImgCosmicMind/blob/main/LogoColorida.png?raw=true";
            string assunto = "COSMIC MIND - Código de Validação";
            string corpo = $"<html><body style='font-family: Arial, sans-serif;'>" +
                $"<center><img src='{urlImagem}' alt='Imagem do Jogo' style='width: 90%; height: auto;'></center>" +
                $"<h1 style='color: #5F3BD3;'>Bem-vindo ao COSMIC MIND!</h1>" +
                $"<p>Seu código de validação é: <strong style='font-size: 24px;'>{codigoValidacao}</strong></p>" +
                $"<p>Obrigado por se cadastrar!</p>" +
                $"<p>Se você não solicitou este código, ignore este e-mail.</p>" +
                $"</body></html>";

            try
            {
                MailMessage mensagem = new MailMessage();
                mensagem.From = new MailAddress(emailRemetente);
                mensagem.To.Add(destinatario);
                mensagem.Subject = assunto;
                mensagem.Body = corpo;
                mensagem.IsBodyHtml = true;

                using (SmtpClient smtpClient = new SmtpClient(smtpServidor, smtpPorta))
                {
                    smtpClient.Credentials = new NetworkCredential(emailRemetente, senhaRemetente);
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mensagem);
                    Debug.Log("[EnviarEmail] E-mail enviado com sucesso.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[EnviarEmail] Erro ao enviar e-mail: {ex.Message}");
            }
        }

        public bool ValidarCodigo(string codigoUsuario)
        {
            string codigoSalvo = PlayerPrefs.GetString("codigoSalvo", "").Trim();
            string codigoInserido = codigoUsuario.Trim();

            Debug.Log($"[EnviarEmail] Validando código: '{codigoInserido}' contra '{codigoSalvo}'");

            return string.Equals(codigoInserido, codigoSalvo, StringComparison.OrdinalIgnoreCase);
        }
    

}
