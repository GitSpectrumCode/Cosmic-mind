using UnityEngine;

public class Avaliacao : MonoBehaviour
{
    private int numEstrelas;
    private int pontuacao;

    public void SalvarAvaliacao(float tempoDecorrido)
    {
        // Calcula estrelas com base no tempo
        if (tempoDecorrido < 200) numEstrelas = 5;
        else if (tempoDecorrido < 400) numEstrelas = 4;
        else if (tempoDecorrido < 600) numEstrelas = 3;
        else numEstrelas = 2;

        // Calcula pontua��o
        pontuacao = numEstrelas * 100;

        // Salva estrelas e pontua��o 
        PlayerPrefs.SetInt("numEstrelas", numEstrelas);
        PlayerPrefs.SetInt("pontuacao", pontuacao);
        PlayerPrefs.Save();

        Debug.Log($"Estrelas: {numEstrelas}, Pontua��o: {pontuacao} salvas no PlayerPrefs.");
    }

    public int CarregarEstrelas()
    {
        return PlayerPrefs.GetInt("numEstrelas", 0); 
    }

    public int CarregarPontuacao()
    {
        return PlayerPrefs.GetInt("pontuacao", 0); 
    }
}
