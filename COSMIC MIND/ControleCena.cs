using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleCena : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup; 
    public float fadeDuration = 1f; 
    public string[] cenas; 
    public float tempoEspera = 3f; 

    void Start()
    {
        StartCoroutine(TransicaoEntreCenas());
    }

    IEnumerator TransicaoEntreCenas()
    {
        for (int i = 0; i < cenas.Length; i++)
        {
            
            yield return new WaitForSeconds(tempoEspera);

            
            yield return StartCoroutine(Fade(1));

            
            SceneManager.LoadScene(cenas[i]);

            yield return StartCoroutine(Fade(0));
        }
    }

    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
