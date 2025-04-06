using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPeca : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public bool isLocked = false; 

    private Vector3 offset; 
    private bool isDragging = false; 

    void Update()
    {
        if (isLocked)
            return; 

        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (touch.phase == TouchPhase.Began)
            {
                // Verifica se o toque começou sobre a peça
                if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPosition))
                {
                    isDragging = true;
                    offset = transform.position - touchPosition;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector3 newPosition = touchPosition + offset;
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeed);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
        // Detecta entrada de mouse em PC
        else if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            if (Input.GetMouseButtonDown(0))
            {
                // Verifica se o clique começou sobre a peça
                if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(mousePosition))
                {
                    isDragging = true;
                    offset = transform.position - mousePosition;
                }
            }
            else if (Input.GetMouseButton(0) && isDragging)
            {
                Vector3 newPosition = mousePosition + offset;
                transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeed);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }
    }

    // Método para travar a peça
    public void LockPiece()
    {
        isLocked = true;
    }
}
