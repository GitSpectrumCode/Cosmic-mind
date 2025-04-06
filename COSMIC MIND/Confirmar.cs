using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Adicione este namespace para gerenciar cenas
using UnityEngine.UI;

public class Confirmar : MonoBehaviour
{
    public GameObject visiblePiece;    // A pe�a vis�vel que o jogador vai mover
    public GameObject invisiblePiece;  // A pe�a invis�vel que define a posi��o correta
    public float tolerance = 0.1f;      // Toler�ncia para verificar a proximidade
    public Canvas confirmationCanvas;  // Canvas com a mensagem de confirma��o

    void Start()
    {
        // Certifique-se de que o Canvas de confirma��o est� escondido no in�cio
        if (confirmationCanvas != null)
        {
            confirmationCanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (visiblePiece != null && invisiblePiece != null)
        {
            float distance = Vector3.Distance(visiblePiece.transform.position, invisiblePiece.transform.position);
            if (distance < tolerance)
            {
                // Se a pe�a estiver na posi��o correta, mostra o Canvas de confirma��o
                if (confirmationCanvas != null)
                {
                    confirmationCanvas.gameObject.SetActive(true);
                }

                // Trava a pe�a
                MoverPeca pieceMovement = visiblePiece.GetComponent<MoverPeca>();
                if (pieceMovement != null)
                {
                    pieceMovement.LockPiece();
                }

                // Desativa a verifica��o adicional
                enabled = false; // Desativa o script para evitar checagens adicionais

                // Inicia a Coroutine para aguardar e mudar de cena
                StartCoroutine(WaitAndLoadScene(2f)); // 1 segundo de espera
            }
            else
            {
                // Ocultar o Canvas se a pe�a n�o estiver na posi��o correta
                if (confirmationCanvas != null)
                {
                    confirmationCanvas.gameObject.SetActive(false);
                }
            }
        }
    }

    // Coroutine para aguardar e carregar a pr�xima cena
    private IEnumerator WaitAndLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // Aguarda o tempo especificado
        SceneManager.LoadScene("quebracabeca"); // Substitua "NomeDaSuaCena" pelo nome da sua cena
    }
}
