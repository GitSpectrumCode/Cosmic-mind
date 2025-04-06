using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador
{
    
    public float tempoTotal; 
    public int pontuacaoTotal; 
    public int estrelasTotais; 

 
    public List<string> fasesDesbloqueadas; 
    public List<string> artefatosColetados; 
    public List<string> mascotesDesbloqueados; 

   
    public Jogador()
    {
        tempoTotal = 0f;
        pontuacaoTotal = 0;
        estrelasTotais = 0;
        fasesDesbloqueadas = new List<string>();
        artefatosColetados = new List<string>();
        mascotesDesbloqueados = new List<string>();
    }
}
