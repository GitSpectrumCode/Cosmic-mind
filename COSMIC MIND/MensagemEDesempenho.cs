using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class MensagemEDesempenho : MonoBehaviour
{
    
    public Text mensagemTexto; 
    public Button botaoAtualizar; 
    public Button botaoDesempenho; 

    void Start()
    {
      
        botaoAtualizar.onClick.AddListener(ExibirMensagem);
        botaoDesempenho.onClick.AddListener(AbrirTelaDesempenho);
    }


    public void ExibirMensagem()
    {
        mensagemTexto.text = "Você já conquistou todos os mascotes";
        mensagemTexto.color = Color.yellow; 
    }


    void AbrirTelaDesempenho()
    {
        SceneManager.LoadScene("TelaDesempenho"); 
    }
}
