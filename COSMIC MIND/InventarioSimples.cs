using UnityEngine;
using UnityEngine.UI;

public class InventarioSimples : MonoBehaviour
{
  
    public Image[] inventarioImagens;

  
    public Sprite[] spritesMascotes;

   
    public Image nuvemImagem;  

    private float tempoCliqueMaximo = 0.5f;  
    private float tempoUltimoClique = 0f;  
    private int indexMascoteAtual = 0;  
    private int contadorCliquesConsecutivos = 0;  

    void Start()
    {
    
        EsconderMascotes();

       
        nuvemImagem.gameObject.SetActive(true);
    }

   
    public void OnBotaoClique()
    {
        
        if (Time.time - tempoUltimoClique <= tempoCliqueMaximo)
        {
            contadorCliquesConsecutivos++;
        }
        else
        {
            contadorCliquesConsecutivos = 1;  
        }

       
        if (contadorCliquesConsecutivos == 3)
        {
            ReiniciarInventario();
            contadorCliquesConsecutivos = 0;  
        }
      
        else if (contadorCliquesConsecutivos == 2)
        {
            MostrarTodosMascotes();
        }
       
        else
        {
            MostrarMascote(indexMascoteAtual);
            indexMascoteAtual = (indexMascoteAtual + 1) % inventarioImagens.Length;  
        }

      
        nuvemImagem.gameObject.SetActive(false);

     
        tempoUltimoClique = Time.time;
    }


    void MostrarMascote(int index)
    {
        if (index >= 0 && index < inventarioImagens.Length)
        {
            inventarioImagens[index].sprite = spritesMascotes[index];  
            inventarioImagens[index].gameObject.SetActive(true);  
        }
    }


    void MostrarTodosMascotes()
    {
        for (int i = 0; i < inventarioImagens.Length; i++)
        {
            inventarioImagens[i].sprite = spritesMascotes[i]; 
            inventarioImagens[i].gameObject.SetActive(true);  
        }
    }


    void EsconderMascotes()
    {
        foreach (Image img in inventarioImagens)
        {
            img.gameObject.SetActive(false);  
        }
    }

    void ReiniciarInventario()
    {
        EsconderMascotes();  
        indexMascoteAtual = 0;  
        nuvemImagem.gameObject.SetActive(true);  
    }
}
