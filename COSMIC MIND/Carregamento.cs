using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Carregamento : MonoBehaviour
{
    public Image barraDeProgresso;
    public Text textoPorcentagem;
    public string nomeDaCena;
    public AudioSource musicaDeFundo; 

    private float progresso = 0f;
    public float velocidadeDePreenchimento = 0.1f;
    public float fadeDuration = 2f; 

    void Start()
    {
        StartCoroutine(CarregarCenaAsync());
    }

    IEnumerator CarregarCenaAsync()
    {
        AsyncOperation operacao = SceneManager.LoadSceneAsync(nomeDaCena);
        operacao.allowSceneActivation = false;

        while (!operacao.isDone)
        {
            progresso = Mathf.MoveTowards(progresso, operacao.progress / 0.9f, velocidadeDePreenchimento * Time.deltaTime);

            barraDeProgresso.fillAmount = progresso;

            textoPorcentagem.text = Mathf.RoundToInt(progresso * 100) + "%";

         
            if (progresso >= 0.9f)
            {
                StartCoroutine(FadeOut(musicaDeFundo, fadeDuration));
            }

            if (progresso >= 1f && operacao.progress >= 0.9f)
            {
                operacao.allowSceneActivation = true;
            }

            yield return null;
        }
    }


    IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        audioSource.Stop(); 
        audioSource.volume = startVolume; 
    }
}
