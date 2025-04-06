using UnityEngine;
using UnityEngine.UI;

public class ExibirMensagemImagem : MonoBehaviour
{
    // Referências para o Text e a Image na UI
   // Referência para o texto da mensagem
    public Image imagem;       // Referência para a imagem

    // Referência para a Sprite da imagem
    public Sprite imagemNuvem; // Sprite da imagem que você quer exibir

    void Start()
    {
        // Exibe a mensagem
       

        // Exibe a imagem
        ExibirImagem(imagemNuvem);
    }

 
    void ExibirImagem(Sprite novaImagem)
    {
        imagem.sprite = novaImagem;  // Define o sprite da imagem
        imagem.gameObject.SetActive(true);  // Torna a imagem visível
    }
}
