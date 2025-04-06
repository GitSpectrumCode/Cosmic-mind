using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GerenciadorInputs : MonoBehaviour, IDropHandler
{
    
    private Dictionary<string, string[]> palavras = new Dictionary<string, string[]>()
    {
        { "MARTE", new string[] { "M", "A", "R", "T", "E" } },
        { "VENUS", new string[] { "V", "E", "N", "U", "S" } },
        { "TERRA", new string[] { "T", "E", "R", "R", "A" } },
        { "JUPITER", new string[] { "J", "U", "P", "I", "T", "E", "R" } },
        { "SATURNO", new string[] { "S", "A", "T", "U", "R", "N", "O" } },
        { "URANO", new string[] { "U", "R", "A", "N", "O" } },
        { "NETUNO", new string[] { "N", "E", "T", "U", "N", "O" } },
        { "MERCURIO", new string[] { "M", "E", "R", "C", "U", "R", "I", "O" } }
    };

    public GameObject painelCompleto; 

    public void OnDrop(PointerEventData eventData)
    {
        if (DragHandler.itemBeingDragged != null)
        {
            
            Text draggedText = DragHandler.itemBeingDragged.GetComponentInChildren<Text>();
            if (draggedText != null)
            {
                string letraArrastada = draggedText.text.ToUpper();

                InputField inputField = GetComponent<InputField>();

                if (inputField != null)
                {
                   
                    if (!inputField.interactable)
                    {
                        Debug.Log("Campo já travado. Não pode ser alterado.");
                        return;
                    }

                    inputField.text = letraArrastada;

                    VerificarLetraCorreta(inputField);
                }
                else
                {
                    Debug.LogError("O InputField não foi encontrado no GameObject.");
                }
            }
            else
            {
                Debug.LogError("Nenhum componente Text foi encontrado no objeto arrastado.");
            }
        }
        else
        {
            Debug.LogError("Nenhum item está sendo arrastado.");
        }
    }

    private void VerificarLetraCorreta(InputField inputField)
    {
        
        string nomeCampo = inputField.name;
        string letraInserida = inputField.text.ToUpper();

        bool letraCorreta = false;

        
        foreach (var palavra in palavras)
        {
            
            for (int i = 0; i < palavra.Value.Length; i++)
            {
               
                string nomeEsperado = palavra.Key + "_" + i;

               
                if (nomeCampo == nomeEsperado && palavra.Value[i] == letraInserida)
                {

                    letraCorreta = true;

                 
                    inputField.interactable = false; 
                    inputField.textComponent.color = Color.green; 

                    Image inputFieldBackground = inputField.GetComponent<Image>();
                    if (inputFieldBackground != null)
                    {
                        inputFieldBackground.color = Color.green; 
                    }

                    Debug.Log("Letra correta: " + letraInserida + " na palavra: " + palavra.Key);
                    break;
                }
            }

            if (letraCorreta) break; 
        }

        if (!letraCorreta)
        {
            Debug.Log("Letra incorreta: " + letraInserida);
           
            inputField.text = ""; 
        }

       
        VerificarPalavras();
    }

    public void VerificarPalavras()
    {
        
        foreach (var palavra in palavras)
        {
            for (int i = 0; i < palavra.Value.Length; i++)
            {
                string nomeCampo = palavra.Key + "_" + i;
                InputField inputField = GameObject.Find(nomeCampo)?.GetComponent<InputField>();

                if (inputField != null && inputField.interactable)
                {
                    
                    return;
                }
            }
        }

        
        if (painelCompleto != null)
        {
            painelCompleto.SetActive(true);
        }
    }
}
