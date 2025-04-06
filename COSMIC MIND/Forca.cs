using UnityEngine;
using UnityEngine.UI;

public class Forca : MonoBehaviour
{
    public string[] palavrasSecretas = { "SOL", "COMETA", "ESTRELA", "PLANETA", "LUA" };  
    public string[] dicas = { "É uma estrela que dá luz e calor.", "Uma bola de gelo que brilha e tem uma cauda.", "Brilha no céu e é muito distante de nós.", "Corpo celeste no sistema solar.", "A bola brilhante que vemos à noite no céu." };  // Array de dicas
    public Text palavraExibida; 
    public Text dicaText;  
    public Text letrasErradasText; 
    public Button[] botoesLetras;  
    public GameObject painelConclusao;  
    public GameObject painelTentarNovamente; 
    public Button botaoTentarNovamente;
    public AudioSource somAcerto;  
    public AudioSource somErro;  

    private string palavraSecreta; 
    private string dicaAtual; 
    private int maxErros = 10;  
    private int erros; 
    private int palavrasDescobertas = 0;  
    private char[] letrasExibidas;  
    private string letrasErradas = "";  

    void Start()
    {
        IniciarNovaPalavra();

        
        foreach (Button botao in botoesLetras)
        {
            botao.onClick.AddListener(() => TentarLetra(botao.GetComponentInChildren<Text>().text[0]));
        }

        
        painelConclusao.SetActive(false);
        painelTentarNovamente.SetActive(false);

       
        botaoTentarNovamente.onClick.AddListener(ReiniciarJogo);
    }

    
    void IniciarNovaPalavra()
    {
        
        if (palavrasDescobertas < palavrasSecretas.Length)
        {
  
            palavraSecreta = palavrasSecretas[palavrasDescobertas];
            dicaAtual = dicas[palavrasDescobertas];

          
            letrasExibidas = new char[palavraSecreta.Length];
            for (int i = 0; i < palavraSecreta.Length; i++)
            {
                letrasExibidas[i] = '_';  
            }
            AtualizarPalavraExibida();

       
            dicaText.text = "Dica: " + dicaAtual;
            dicaText.color = Color.green; 

        
            letrasErradasText.text = "Letras Erradas: "; 
            erros = 0;
            letrasErradas = "";

         
            dicaText.gameObject.SetActive(true);
            letrasErradasText.gameObject.SetActive(true);
        }
    }

   
    public void TentarLetra(char letra)
    {
        bool acertou = false;

       
        for (int i = 0; i < palavraSecreta.Length; i++)
        {
            if (char.ToUpper(palavraSecreta[i]) == letra)
            {
                letrasExibidas[i] = letra;  
                acertou = true;
            }
        }

        if (acertou)
        {
            somAcerto.Play(); 
        }
        else
        {
            erros++;  
            letrasErradas += letra + " "; 
            AtualizarLetrasErradas();  
            somErro.Play();  
        }

        AtualizarPalavraExibida();

       
        if (new string(letrasExibidas) == palavraSecreta)
        {
            palavrasDescobertas++;  
            if (palavrasDescobertas < palavrasSecretas.Length)
            {
                GanharPalavra();  
            }
            else
            {
                GanharJogo();  
            }
        }
        else if (erros >= maxErros)
        {
            PerderJogo();
        }
    }

   
    void AtualizarPalavraExibida()
    {
        palavraExibida.text = string.Join(" ", letrasExibidas);  
    }

    void AtualizarLetrasErradas()
    {
        letrasErradasText.text = "Letras Erradas: " + letrasErradas;
        letrasErradasText.color = Color.red;  
    }

   
    void GanharPalavra()
    {
        palavraExibida.text = palavraSecreta;  

       
        dicaText.gameObject.SetActive(false);
        letrasErradasText.gameObject.SetActive(false);

        Invoke("IniciarNovaPalavra", 2f);  
    }

    void GanharJogo()
    {
        palavraExibida.text = palavraSecreta;  
        letrasErradasText.text = "Letras Erradas: " + letrasErradas;  
        letrasErradasText.color = Color.white;  

        
        Debug.Log("Jogo Concluído! Exibindo painel de conclusão...");

      
        painelConclusao.SetActive(true);
    }

 
    void PerderJogo()
    {
        palavraExibida.text = "Você perdeu! A palavra era: " + palavraSecreta;
        letrasErradasText.text = "Letras Erradas: " + letrasErradas;  
        letrasErradasText.color = Color.white; 

        
        painelTentarNovamente.SetActive(true);
    }


    void ReiniciarJogo()
    {
        painelTentarNovamente.SetActive(false);  
        palavrasDescobertas = 0;  
        IniciarNovaPalavra();  
    }
}
