using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private Vector3 startPosition;
    private bool isDragging = false;
    private bool isCorrectPosition = false;

    public Vector3[] finalPositions; 
    public int pieceIndex; 

    private static List<int> correctPieceIndices = new List<int>();

    void Start()
    {
        startPosition = transform.position; 

     
        if (pieceIndex < 0 || pieceIndex >= finalPositions.Length)
        {
            Debug.LogError("Índice da peça é inválido. Por favor, configure corretamente no Inspector.");
        }

       
        if (!correctPieceIndices.Contains(pieceIndex))
        {
            correctPieceIndices.Add(pieceIndex);
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f; 
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }

    private void OnMouseDown()
    {
        if (!isCorrectPosition)
        {
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

   
        Vector3 finalPosition = finalPositions[pieceIndex];
        if (Vector3.Distance(transform.position, finalPosition) < 1.500f)
        {
            transform.position = finalPosition;
            isCorrectPosition = true;

            CheckAllPiecesCorrect();
        }
        else
        {
            transform.position = startPosition;
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        isCorrectPosition = false;
    }

    private void CheckAllPiecesCorrect()
    {
        // Verifica se todas as peças estão na posição correta
        bool allCorrect = true;
        foreach (var piece in FindObjectsOfType<DragDrop>())
        {
            if (!piece.isCorrectPosition)
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            GameManagerQuebraC.Instance.ShowEndGameUI();
        }
    }
}
