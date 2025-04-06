using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicTacToeManager : MonoBehaviour
{
    public Button[] buttons; 
    public TextMeshProUGUI resultText; 
    public GameObject starContainer; 
    public GameObject starPrefab; 
    public Button restartButton; 
    public Button nextButton; 
    public Sprite xSprite; 
    public Sprite oSprite; 
    private string[,] board; 
    private string currentPlayer = "X"; 
    private bool gameOver = false; 
    public GameObject resultPanel; 

    private float gameStartTime; 

    void Start()
    {
        board = new string[3, 3];

        
        resultText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        resultPanel.SetActive(false); 
        starContainer.SetActive(false); 

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => PlayerMove(index));
            buttons[i].GetComponent<Image>().sprite = null;
            buttons[i].interactable = true;
        }

        gameStartTime = Time.time; 
    }

    void PlayerMove(int index)
    {
        if (gameOver) return;

        int row = index / 3;
        int col = index % 3;

        if (board[row, col] != null)
        {
            Debug.LogWarning("Célula já preenchida!");
            return;
        }

        board[row, col] = currentPlayer;

        buttons[index].GetComponent<Image>().sprite = currentPlayer == "X" ? xSprite : oSprite;
        buttons[index].interactable = false;

        if (CheckWin(currentPlayer))
        {
            EndGame(currentPlayer);
            return;
        }

        if (IsBoardFull())
        {
            EndGame(null); 
            return;
        }

        currentPlayer = currentPlayer == "X" ? "O" : "X";

        if (currentPlayer == "O" && !gameOver)
        {
            Invoke("BotMove", 0.5f);
        }
    }

    void BotMove()
    {
        int index = GetRandomMove();
        PlayerMove(index);
    }

    int GetRandomMove()
    {
        var availableMoves = new System.Collections.Generic.List<int>();
        for (int i = 0; i < buttons.Length; i++)
        {
            int row = i / 3;
            int col = i % 3;

            if (board[row, col] == null)
            {
                availableMoves.Add(i);
            }
        }
        int randomIndex = Random.Range(0, availableMoves.Count);
        return availableMoves[randomIndex];
    }

    void EndGame(string winner)
    {
        gameOver = true;

        
        resultPanel.SetActive(true);
        resultText.gameObject.SetActive(true);
        starContainer.SetActive(true); 

        float timeTaken = Time.time - gameStartTime; 

        if (winner == "X")
        {
            resultText.text = "Paulinho venceu!";
            ShowStarsBasedOnTime(timeTaken); 
            nextButton.gameObject.SetActive(true); 
        }
        else if (winner == "O")
        {
            resultText.text = "John Cyber venceu!";
            ShowStars(0); 
        }
        else
        {
            resultText.text = "Empate!";
            ShowStars(1); 
        }

        restartButton.gameObject.SetActive(true); 

        foreach (Button btn in buttons)
        {
            btn.interactable = false;
        }
    }

    void ShowStars(int count)
    {
      
        if (starContainer == null || starPrefab == null)
        {
            Debug.LogError("StarContainer ou StarPrefab não atribuídos!");
            return;
        }

       
        foreach (Transform child in starContainer.transform)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }

      
        for (int i = 0; i < count; i++)
        {
            Instantiate(starPrefab, starContainer.transform);
        }
    }

    void ShowStarsBasedOnTime(float timeTaken)
    {
        
        if (starContainer == null || starPrefab == null)
        {
            Debug.LogError("StarContainer ou StarPrefab não atribuídos!");
            return;
        }

 
        foreach (Transform child in starContainer.transform)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }

     
        int starCount = 0;
        if (timeTaken <= 2)
        {
            starCount = 3; 
        }
        else if (timeTaken <= 5)
        {
            starCount = 2; 
        }
        else
        {
            starCount = 1; 
        }

        
        starContainer.SetActive(true);

        
        for (int i = 0; i < starCount; i++)
        {
            Instantiate(starPrefab, starContainer.transform);
        }
    }

    bool CheckWin(string player)
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) return true;
            if (board[0, i] == player && board[1, i] == player && board[2, i] == player) return true;
        }
        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) return true;
        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player) return true;

        return false;
    }

    bool IsBoardFull()
    {
        foreach (string cell in board)
        {
            if (cell == null) return false;
        }
        return true;
    }

    public void RestartGame()
    {
        board = new string[3, 3];

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().sprite = null;
            buttons[i].interactable = true;
        }

        currentPlayer = "X";

        resultText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        resultPanel.SetActive(false); 
        starContainer.SetActive(false); 

        gameOver = false;
        gameStartTime = Time.time;
    }

    public void AdvanceGame()
    {
        Debug.Log("Avançar para a próxima fase!");
      
    }
}
