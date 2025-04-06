using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Adicione este namespace para gerenciar cenas
using UnityEngine.UI;

public class Confirmar : MonoBehaviour
{
    public GameObject visiblePiece;    // A peça visível que o jogador vai mover
    public GameObject invisiblePiece;  // A peça invisível que define a posição correta
    public float tolerance = 0.1f;      // Tolerância para verificar a proximidade
    public Canvas confirmationCanvas;  // Canvas com a mensagem de confirmação

    void Start()
    {
        // Certifique-se de que o Canvas de confirmação está escondido no início
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
                // Se a peça estiver na posição correta, mostra o Canvas de confirmação
                if (confirmationCanvas != null)
                {
                    confirmationCanvas.gameObject.SetActive(true);
                }

                // Trava a peça
                MoverPeca pieceMovement = visiblePiece.GetComponent<MoverPeca>();
                if (pieceMovement != null)
                {
                    pieceMovement.LockPiece();
                }

                // Desativa a verificação adicional
                enabled = false; // Desativa o script para evitar checagens adicionais

                // Inicia a Coroutine para aguardar e mudar de cena
                StartCoroutine(WaitAndLoadScene(2f)); // 1 segundo de espera
            }
            else
            {
                // Ocultar o Canvas se a peça não estiver na posição correta
                if (confirmationCanvas != null)
                {
                    confirmationCanvas.gameObject.SetActive(false);
                }
            }
        }
    }

    // Coroutine para aguardar e carregar a próxima cena
    private IEnumerator WaitAndLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // Aguarda o tempo especificado
        SceneManager.LoadScene("quebracabeca"); // Substitua "NomeDaSuaCena" pelo nome da sua cena
    }
}
