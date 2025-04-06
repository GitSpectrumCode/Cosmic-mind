using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cronometro1 : MonoBehaviour
{
    private float tempoDecorrido = 0f;
    private bool contando = false;
    private bool pausado = false;

    public Text textoCronometro;
    public GameObject painelPausa;
    public Button botaoPausar;
    public Sprite spritePausar;
    public Sprite spriteContinuar;

    public float tempoLimite = 660f; // Limite de tempo (11 minutos)
    private Avaliacao avaliacao;

    private void Start()
    {
        avaliacao = GetComponent<Avaliacao>();
        CarregarTempoDecorrido(); 
        IniciarCronometro();
        painelPausa.SetActive(false);
        AtualizarBotaoPausar();
    }

    public void IniciarCronometro()
    {
        tempoDecorrido = 0f;  
        contando = true;
        pausado = false;
        painelPausa.SetActive(false);
        AtualizarBotaoPausar();
    }

    public void PausarCronometro()
    {
        if (contando)
        {
            pausado = true;
            contando = false;
            painelPausa.SetActive(true);
            AtualizarBotaoPausar();
            SalvarTempoDecorrido(); 
        }
    }

    public void ContinuarCronometro()
    {
        if (pausado)
        {
            pausado = false;
            contando = true;
            painelPausa.SetActive(false);
            AtualizarBotaoPausar();
        }
    }

    public void PararCronometro()
    {
        contando = false;
        pausado = false;
    }

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void Update()
    {
        if (contando)
        {
            tempoDecorrido += Time.deltaTime;

            if (tempoDecorrido >= tempoLimite)
            {
                tempoDecorrido = tempoLimite;
                PararCronometro();
                ConcluirNivel();
                SalvarTempoDecorrido();
            }
        }

        AtualizarTextoCronometro();
    }

    private void AtualizarTextoCronometro()
    {
        int minutos = Mathf.FloorToInt(tempoDecorrido / 60f);
        int segundos = Mathf.FloorToInt(tempoDecorrido % 60f);
        textoCronometro.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    private void AtualizarBotaoPausar()
    {
        botaoPausar.image.sprite = pausado ? spritePausar : spriteContinuar;
    }

    private void ConcluirNivel()
    {
        avaliacao.SalvarAvaliacao(tempoDecorrido);
        SceneManager.LoadScene("TelaFinal");
    }

    private void SalvarTempoDecorrido()
    {
        PlayerPrefs.SetFloat("tempoDecorrido", tempoDecorrido);
        PlayerPrefs.Save();
    }

    private void CarregarTempoDecorrido()
    {
        tempoDecorrido = PlayerPrefs.GetFloat("tempoDecorrido", 0f);
    }
}
