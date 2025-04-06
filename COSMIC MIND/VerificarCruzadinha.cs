using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerificarCruzadinha : MonoBehaviour
{
    public GameObject painelCompleto; 
    public GameObject painelProximo; 
    public List<InputField> campos; 
    public Button botaoProsseguir; 
    public Text textoPontuacao; 
    public Text textoTempoFinal; 
    public Text textoTemporizador; 

    private float tempoInicio; 
    private float tempoAtual; 
    private bool jogoEmAndamento = true; 
    private int pontuacao; 

    private void Start()
    {
        if (painelCompleto == null)
        {
            Debug.LogError("Painel Completo não atribuído!");
        }

        if (painelProximo == null)
        {
            Debug.LogError("Painel Próximo não atribuído!");
        }

        painelCompleto.SetActive(false); 
        painelProximo.SetActive(false); 

        tempoInicio = Time.time; 

     
        foreach (var campo in campos)
        {
            campo.text = string.Empty;
        }

        
        if (botaoProsseguir != null)
        {
            botaoProsseguir.onClick.AddListener(MostrarPainelProximo);
        }
    }

    private void Update()
    {
        if (jogoEmAndamento)
        {
            
            tempoAtual = Time.time - tempoInicio;


            if (textoTemporizador != null)
            {
                textoTemporizador.text = FormatTempo(tempoAtual);
            }

        
            if (TodosCamposCorretos())
            {
                FinalizarJogo();
            }
        }
    }

    private bool TodosCamposCorretos()
    {
        foreach (var campo in campos)
        {
         
            if (string.IsNullOrWhiteSpace(campo.text) || !EhTextoValido(campo.text))
            {
                return false; 
            }
        }

       
        return true;
    }

    private bool EhTextoValido(string texto)
    {
      
        foreach (char c in texto)
        {
            if (!char.IsLetter(c))
            {
                return false;
            }
        }
        return true;
    }

    private void FinalizarJogo()
    {
        if (!jogoEmAndamento) return; 

     
        jogoEmAndamento = false;
        float tempoFinal = tempoAtual; 

      
        CalcularPontuacao(tempoFinal);

    
        painelCompleto.SetActive(true);

        
        if (textoTempoFinal != null)
        {
            textoTempoFinal.text = "Tempo: " + FormatTempo(tempoFinal);
        }

        if (textoPontuacao != null)
        {
            textoPontuacao.text = "Pontuação: " + pontuacao.ToString();
        }

        Debug.Log("Jogo finalizado! Tempo: " + tempoFinal + " Pontuação: " + pontuacao);
    }

    private void CalcularPontuacao(float tempoFinal)
    {
      
        pontuacao = Mathf.Max(0, 1000 - Mathf.RoundToInt(tempoFinal * 10));
    }

    private string FormatTempo(float tempo)
    {
        int minutos = Mathf.FloorToInt(tempo / 60);
        int segundos = Mathf.FloorToInt(tempo % 60);
        return string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void MostrarPainelProximo()
    {
        Debug.Log("Mostrando próximo painel...");

        
        painelCompleto.SetActive(false);
        painelProximo.SetActive(true);
    }
}
