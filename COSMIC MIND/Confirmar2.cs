using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;

public class Confirmar2 : MonoBehaviour
{
    public GameObject visiblePiece;    
    public GameObject invisiblePiece;  
    public float tolerance = 0.1f;    
    public Canvas confirmationCanvas;  

    void Start()
    {
       
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
                
                if (confirmationCanvas != null)
                {
                    confirmationCanvas.gameObject.SetActive(true);
                }

                
                MoverPeca pieceMovement = visiblePiece.GetComponent<MoverPeca>();
                if (pieceMovement != null)
                {
                    pieceMovement.LockPiece();
                }

                
                enabled = false; 

            }
            else
            {
              
                if (confirmationCanvas != null)
                {
                    confirmationCanvas.gameObject.SetActive(false);
                }
            }
        }
    }
}