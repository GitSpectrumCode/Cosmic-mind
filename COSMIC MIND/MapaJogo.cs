using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapaJogo : MonoBehaviour
{
    public Button[] planetas; 
    public GameObject[] cadeados; 
    public Button botaoInvisivel; 
    public string[] cenasPlanetas; 
    private int indiceDesbloqueio = 0; 

    void Start()
    {
       
        for (int i = 0; i < planetas.Length; i++)
        {
            int planetaIndex = i; 
            planetas[i].onClick.AddListener(() => CarregarCena(planetaIndex)); 
        }

   
        botaoInvisivel.onClick.AddListener(DesbloquearProximoPlaneta);
    }

    // Função que desbloqueia o próximo planeta, removendo o cadeado
    void DesbloquearProximoPlaneta()
    {
       
        if (indiceDesbloqueio < cadeados.Length)
        {
           
            cadeados[indiceDesbloqueio].SetActive(false);
           
            indiceDesbloqueio++;
        }
    }

    // Função que carrega a cena correspondente ao planeta
    void CarregarCena(int planetaIndex)
    {
        // Certifica-se de que o índice está dentro dos limites da lista de cenas
        if (planetaIndex >= 0 && planetaIndex < cenasPlanetas.Length)
        {
            SceneManager.LoadScene(cenasPlanetas[planetaIndex]); // Carrega a cena correspondente ao nome no array de cenas
        }
        else
        {
            Debug.LogError("Índice de planeta fora do alcance ou cena não configurada!");
        }
    }
}
