using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public GameObject telaDePausa;       
    public GameObject painelFinal;       
    public Text pontuacaoText;           
    
    public List<DragAndDrop> pecas;      

    private int pontuacaoBase = 100;    
    private int pontuacao;               
    private int erros;                   
    private float tempoInicio;           

    void Start()
    {
        tempoInicio = Time.time;         
        pontuacao = pontuacaoBase;       
        erros = 0;
        
    }

    void Update()
    {
        VerificarTodasPecasPosicionadas(); 
    }

   
    public void RegistrarErro()
    {
        erros++;
        pontuacao -= 50; 
        
    }

    

    void VerificarTodasPecasPosicionadas()
    {
        foreach (DragAndDrop peca in pecas)
        {
            if (!peca.estaPosicionadaCorretamente)
            {
                return; 
            }
        }

        
        FinalizarJogo();
    }

  
    void FinalizarJogo()
    {
        float tempoDecorrido = Time.time - tempoInicio;
        int descontoPorTempo = Mathf.RoundToInt(tempoDecorrido * 2); 

       
        pontuacao = Mathf.Max(pontuacaoBase - descontoPorTempo - (erros * 50), 0);

        if (pontuacaoText != null)
        {
            pontuacaoText.text = $"Pontuação Final: {pontuacao}\nErros: {erros}\nTempo: {Mathf.RoundToInt(tempoDecorrido)}s";
        }

        painelFinal.SetActive(true); 
        Time.timeScale = 0;        
    }

    
    public void PausarJogo()
    {
        telaDePausa.SetActive(true); 
        Time.timeScale = 0;          
    }

  
    public void RetomarJogo()
    {
        telaDePausa.SetActive(false); 
        Time.timeScale = 1;           
    }

   
    public void RefazerNivel()
    {
        telaDePausa.SetActive(false); 
        Time.timeScale = 1;           

        
        foreach (DragAndDrop peca in pecas)
        {
            peca.ResetarPosicao();
        }

      
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
