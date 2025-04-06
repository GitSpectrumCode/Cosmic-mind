using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerQuebraC : MonoBehaviour
{
    public static GameManagerQuebraC Instance;

    public GameObject restartButton; 
    public GameObject endGameImage; 

    private DragDrop[] puzzlePieces;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

       
        restartButton.SetActive(false);
        endGameImage.SetActive(false);

        
        UpdatePuzzlePieces();
    }

    public void ShowEndGameUI()
    {
        restartButton.SetActive(true);
        endGameImage.SetActive(true);
    }

    public void RestartGame()
    {
       
        restartButton.SetActive(false);
        endGameImage.SetActive(false);

       
        UpdatePuzzlePieces();

        foreach (var piece in puzzlePieces)
        {
            piece.ResetPosition();
        }

        
    }

    private void UpdatePuzzlePieces()
    {
        puzzlePieces = FindObjectsOfType<DragDrop>();
    }
}
