using UnityEngine;
using UnityEngine.UI;

public class ExibirMensagemImagem : MonoBehaviour
{
    // Refer�ncias para o Text e a Image na UI
   // Refer�ncia para o texto da mensagem
    public Image imagem;       // Refer�ncia para a imagem

    // Refer�ncia para a Sprite da imagem
    public Sprite imagemNuvem; // Sprite da imagem que voc� quer exibir

    void Start()
    {
        // Exibe a mensagem
       

        // Exibe a imagem
        ExibirImagem(imagemNuvem);
    }

 
    void ExibirImagem(Sprite novaImagem)
    {
        imagem.sprite = novaImagem;  // Define o sprite da imagem
        imagem.gameObject.SetActive(true);  // Torna a imagem vis�vel
    }
}
