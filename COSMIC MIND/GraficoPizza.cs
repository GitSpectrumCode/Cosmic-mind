using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GraficoPizza : MonoBehaviour
{
    [SerializeField] private Image[] barras;
    [SerializeField] private Text[] legendas;
    [SerializeField] private Text mensagemAtualizacao;
    [SerializeField] private Button botaoAtualizar;

    private int[] valores;
    private bool dadosAtualizados = false; 
    private const string ULTIMA_ATUALIZACAO_KEY = "UltimaAtualizacao_";
    private const string MENSAGEM_SALVA_KEY = "MensagemAtualizacao_";

    private string emailUsuario;

    private void Start()
    {
        botaoAtualizar.onClick.AddListener(AtualizarDados);

   
        emailUsuario = PlayerPrefs.GetString("Email");

        // Verifica se há uma atualização salva para o usuário com base no email
        string dataSalva = PlayerPrefs.GetString(ULTIMA_ATUALIZACAO_KEY + emailUsuario, string.Empty);
        if (!string.IsNullOrEmpty(dataSalva))
        {
            mensagemAtualizacao.text = $"Última atualização: {dataSalva}";
            mensagemAtualizacao.color = Color.green;
            dadosAtualizados = true;  // Marca como atualizado, para evitar mensagem de "não há dados"
        }
        else
        {
            // Inicializa com a mensagem de "sem dados suficientes" até que seja clicado
            mensagemAtualizacao.text = "Nenhuma atualização disponível";
            mensagemAtualizacao.color = Color.red;
        }

        valores = new int[barras.Length];
        AtualizarGraficoBarras(); // Exibe o gráfico vazio ou com valores antigos (se houver)
    }

    private void GerarValoresAleatorios()
    {
        for (int i = 0; i < valores.Length; i++)
        {
            valores[i] = UnityEngine.Random.Range(1, 101);
        }

       
        valores[2] = UnityEngine.Random.Range(80, 101); 
    }

    private void AtualizarGraficoBarras()
    {
        for (int i = 0; i < barras.Length; i++)
        {
            StartCoroutine(AnimarBarra(barras[i], barras[i].fillAmount, valores[i] / 100f, 0.5f));
            legendas[i].text = $"{valores[i]}%";
        }
    }

    private IEnumerator AnimarBarra(Image barra, float valorInicial, float valorFinal, float duracao)
    {
        float tempo = 0f;
        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            float proporcao = tempo / duracao;
            barra.fillAmount = Mathf.Lerp(valorInicial, valorFinal, proporcao);
            yield return null;
        }
        barra.fillAmount = valorFinal;
    }

    public void AtualizarDados()
    {
        if (!dadosAtualizados)
        {
            
            mensagemAtualizacao.text = "Não há dados suficientes!";
            mensagemAtualizacao.color = Color.red;
            dadosAtualizados = true; 
        }
        else
        {
            GerarValoresAleatorios();
            AtualizarGraficoBarras();

            
            string dataAtual = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            mensagemAtualizacao.text = $"Última atualização: {dataAtual}";
            mensagemAtualizacao.color = Color.green;

           
            PlayerPrefs.SetString(ULTIMA_ATUALIZACAO_KEY + emailUsuario, dataAtual);
            PlayerPrefs.SetString(MENSAGEM_SALVA_KEY + emailUsuario, mensagemAtualizacao.text);
            PlayerPrefs.Save();
        }
    }

    private void OnApplicationQuit()
    {
       
        PlayerPrefs.SetString(MENSAGEM_SALVA_KEY + emailUsuario, mensagemAtualizacao.text);
        PlayerPrefs.Save();
    }
}
