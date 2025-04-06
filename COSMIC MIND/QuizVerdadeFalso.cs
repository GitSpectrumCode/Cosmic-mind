using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizVerdadeFalso : MonoBehaviour
{
    [System.Serializable]
    public class Pergunta
    {
        public string textoPergunta;
        public string[] opcoes;
        public int respostaCorreta;
    }

    public Pergunta[] perguntas;
    private List<Pergunta> perguntasEmbaralhadas; 
    private int perguntaAtual = 0;

    public Text textoPergunta;
    public Button[] botoesOpcoes;
    public Text textoProgresso;
    public GameObject botaoRefazer;

    public GameObject painelFinal; 
    public Image spriteFinal;
    public Text textoFinal; 
    public Button botaoAvancar; 

    public GameObject painelNovo; 
    public float tempoParaProximaPergunta = 2f; 

    public AudioClip somClique; 
    public AudioClip somCorreto; 
    public AudioClip somErrado; 
    private AudioSource audioSource;


    public Text textoFeedback;
    private int acertos = 0; 
    private float tempoTotal = 0f; 
    private float tempoRestante; 

    void Start()
    {
        botaoRefazer.SetActive(false);
        painelFinal.SetActive(false); 
        botaoAvancar.gameObject.SetActive(false); 
        painelNovo.SetActive(false); 
        audioSource = GetComponent<AudioSource>();

       
        EmbaralharPerguntas();

        tempoRestante = 10f; 
        MostrarPergunta();
        AtualizarProgresso();
    }

   
    void EmbaralharPerguntas()
    {
        perguntasEmbaralhadas = new List<Pergunta>(perguntas);
        for (int i = perguntasEmbaralhadas.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Pergunta temp = perguntasEmbaralhadas[i];
            perguntasEmbaralhadas[i] = perguntasEmbaralhadas[j];
            perguntasEmbaralhadas[j] = temp;
        }
    }

    
    void MostrarPergunta()
    {
        if (perguntaAtual < perguntasEmbaralhadas.Count)
        {
            Pergunta atual = perguntasEmbaralhadas[perguntaAtual];
          
            textoPergunta.text = "Pergunta " + (perguntaAtual + 1) + ": " + atual.textoPergunta;

           
            for (int i = 0; i < botoesOpcoes.Length; i++)
            {
                if (i < atual.opcoes.Length)
                {
                    botoesOpcoes[i].gameObject.SetActive(true);
                    botoesOpcoes[i].GetComponentInChildren<Text>().text = atual.opcoes[i];
                    botoesOpcoes[i].interactable = true; 
                    botoesOpcoes[i].GetComponent<Image>().color = Color.white; 
                }
                else
                {
                   
                    botoesOpcoes[i].gameObject.SetActive(false);
                }
            }

          
            if (textoFeedback != null)
            {
                textoFeedback.text = "";
            }
        }
    }

   
    void AtualizarProgresso()
    {
        textoProgresso.text = "Pergunta " + (perguntaAtual + 1) + " de " + perguntasEmbaralhadas.Count;
    }

   
    public void Responder(int indiceOpcao)
    {
       
        audioSource.PlayOneShot(somClique);

        
        foreach (Button botao in botoesOpcoes)
        {
            botao.interactable = false;
        }

      
        if (indiceOpcao == perguntasEmbaralhadas[perguntaAtual].respostaCorreta)
        {
            botoesOpcoes[indiceOpcao].GetComponent<Image>().color = Color.green; 
            audioSource.PlayOneShot(somCorreto); 

            acertos++;
        }
        else
        {
            botoesOpcoes[indiceOpcao].GetComponent<Image>().color = Color.red; 
            audioSource.PlayOneShot(somErrado); 
        }

       
        if (textoFeedback != null)
        {
            textoFeedback.text = (indiceOpcao == perguntasEmbaralhadas[perguntaAtual].respostaCorreta) ? "Correto!" : "Errado!";
            textoFeedback.color = (indiceOpcao == perguntasEmbaralhadas[perguntaAtual].respostaCorreta) ? Color.green : Color.red;
        }

       
        Invoke("Avancar", tempoParaProximaPergunta);
    }

    
    void Avancar()
    {
        perguntaAtual++;

       
        tempoTotal += tempoRestante;

        if (perguntaAtual < perguntasEmbaralhadas.Count)
        {
            tempoRestante = 10f; 
            MostrarPergunta();
            AtualizarProgresso();
        }
        else
        {
       
            ExibirPainelFinal();
        }
    }

   
    void ExibirPainelFinal()
    {
        painelFinal.SetActive(true); 
        int pontuacaoFinal = CalcularPontuacao(); 
        textoFinal.text = "Pontuação: " + pontuacaoFinal; 
        botaoAvancar.gameObject.SetActive(true); 
        botaoRefazer.SetActive(true);
    }

  
    int CalcularPontuacao()
    {
        
        int pontosPorAcerto = 10;
        int pontosPorTempo = Mathf.FloorToInt(100 - tempoTotal); 

        int pontuacaoFinal = acertos * pontosPorAcerto + Mathf.Max(0, pontosPorTempo); 
        return pontuacaoFinal;
    }


    public void AvancarFinal()
    {
        Debug.Log("Botão Continuar pressionado!"); 

   
        if (painelFinal != null) painelFinal.SetActive(false);
        if (painelNovo != null) painelNovo.SetActive(true);
        else Debug.LogWarning("painelNovo não está atribuído no Inspector!");
    }



    public void Refazer()
    {
        perguntaAtual = 0;
        acertos = 0; 
        tempoTotal = 0f; 
        painelFinal.SetActive(false); 
        EmbaralharPerguntas(); 
        MostrarPergunta();
        AtualizarProgresso();
        botaoRefazer.SetActive(false);
        botaoAvancar.gameObject.SetActive(false); 
    }
}
