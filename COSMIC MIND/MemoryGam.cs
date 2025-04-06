using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MemoryGameWithTime : MonoBehaviour
{
    public Button[] buttons;  
    public Sprite[] cardImages;  
    private Button firstButton, secondButton; 
    private int correctPairs = 0; 
    private float gameTime = 0f;  
    private bool gameActive = true;  
    private List<Sprite> shuffledImages = new List<Sprite>();  

    public Text timeText;
    public Text scoreText;  
    public GameObject scorePanel;  
    public Text finalScoreText; 

    public Button continueButton; 
    public GameObject nextPanel; 

    void Start()
    {
        ShuffleCards(); 
        StartCoroutine(UpdateTimer());  

     
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueClicked);
        }

      
        scorePanel.SetActive(false);
        if (nextPanel != null) nextPanel.SetActive(false);
    }

    void ShuffleCards()
    {
        shuffledImages.Add(cardImages[1]);
        shuffledImages.Add(cardImages[1]);
        shuffledImages.Add(cardImages[2]);
        shuffledImages.Add(cardImages[2]);
        shuffledImages.Add(cardImages[3]);
        shuffledImages.Add(cardImages[3]);

        System.Random rand = new System.Random();
        for (int i = 0; i < shuffledImages.Count; i++)
        {
            int j = rand.Next(i, shuffledImages.Count);
            Sprite temp = shuffledImages[i];
            shuffledImages[i] = shuffledImages[j];
            shuffledImages[j] = temp;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.sprite = cardImages[0]; 
            int index = i;
            buttons[i].onClick.AddListener(() => OnCardClicked(buttons[index], shuffledImages[index]));
        }
    }

    void OnCardClicked(Button clickedButton, Sprite cardImage)
    {
        if (!gameActive) return;

        if (firstButton == null)
        {
            firstButton = clickedButton;
            firstButton.image.sprite = cardImage;
        }
        else if (secondButton == null && clickedButton != firstButton)
        {
            secondButton = clickedButton;
            secondButton.image.sprite = cardImage;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstButton.image.sprite == secondButton.image.sprite)
        {
            correctPairs++;

            if (correctPairs == 3)
            {
                gameActive = false;
                CalculateScore();
            }
        }
        else
        {
            firstButton.image.sprite = cardImages[0];
            secondButton.image.sprite = cardImages[0];
        }

        firstButton = secondButton = null;
    }

    IEnumerator UpdateTimer()
    {
        while (gameActive)
        {
            gameTime += Time.deltaTime;
            timeText.text = "Tempo: " + Mathf.Round(gameTime).ToString() + "s";
            yield return null;
        }
    }

    void CalculateScore()
    {
        int score = 0;

        if (gameTime <= 30)
        {
            score = 100;
        }
        else if (gameTime <= 60)
        {
            score = 80;
        }
        else if (gameTime <= 90)
        {
            score = 60;
        }
        else if (gameTime <= 120)
        {
            score = 40;
        }
        else if (gameTime <= 150)
        {
            score = 20;
        }

        scorePanel.SetActive(true);  
        finalScoreText.text = "Pontuação: " + score.ToString();
        timeText.text = "";  
    }

    void OnContinueClicked()
    {
        Debug.Log("Botão Continuar pressionado!"); 

      
        if (scorePanel != null) scorePanel.SetActive(false);
        if (nextPanel != null) nextPanel.SetActive(true);
        else Debug.LogWarning("nextPanel não está atribuído no Inspector!");
    }
}
