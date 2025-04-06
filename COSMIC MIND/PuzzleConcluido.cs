using UnityEngine;
using UnityEngine.UI; 

public class PuzzleConcluido : MonoBehaviour
{
    public Text textoEstrelas;  
    public Text textoPontuacao;  
    public Jogador jogador;

    public string[] artefatos = { "Artefato 1", "Artefato 2", "Artefato 3", "Artefato 4", "Artefato 5", "Artefato 6", "Artefato 7", "Artefato 8" };
    public string[] fases = { "Fase 1", "Fase 2", "Fase 3", "Fase 4", "Fase 5", "Fase 6", "Fase 7", "Fase 8", "Fase 9" }; 

    private int faseAtual = 0;

    // Método para finalizar o puzzle
    public void FinalizarPuzzle()
    {
       
        float tempo = PlayerPrefs.GetFloat("tempoFase" + faseAtual, 0f);  

      
        int estrelas = CalcularEstrelas(tempo);

        jogador.estrelasTotais += estrelas;

       
        SalvarEstrelas("estrelasFase" + faseAtual, estrelas);

 
        AtualizarUI(estrelas);

       
        if (estrelas >= 3 && faseAtual < artefatos.Length)
        {
            PlayerPrefs.SetString("artefato" + faseAtual, artefatos[faseAtual]);
            faseAtual++;
        }

        
        Debug.Log($"Puzzle finalizado. Tempo: {tempo}s, Estrelas: {estrelas}, Pontuação: {estrelas * 100}.");
    }

  
    private int CalcularEstrelas(float tempo)
    {
        if (tempo <= 180f)  
            return 5;
        else if (tempo <= 300f)  
            return 4;
        else if (tempo <= 480f)  
            return 3;
        else if (tempo <= 540f)  
            return 2;
        else if (tempo <= 660f)  
            return 1;
        else
            return 0; 
    }

    
    private void SalvarEstrelas(string chave, int valor)
    {
        PlayerPrefs.SetInt(chave, valor);
        PlayerPrefs.Save();
    }

    // Método para atualizar a UI com as estrelas e a pontuação
    private void AtualizarUI(int estrelas)
    {
        if (textoEstrelas != null)
        {
            textoEstrelas.text = "Estrelas: " + estrelas;
        }

        int pontuacao = estrelas * 100;
        if (textoPontuacao != null)
        {
            textoPontuacao.text = "Pontuação: " + pontuacao;
        }
    }

    // Método para resgatar as estrelas salvas 
    public int ResgatarEstrelas(string chave)
    {
        return PlayerPrefs.GetInt(chave, 0);  
    }
}
