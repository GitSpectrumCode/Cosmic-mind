using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    void Start()
    {
       
        StartCoroutine(CarregarProximaCena());
    }

    IEnumerator CarregarProximaCena()
    {
       
        yield return new WaitForSeconds(3);

        
        SceneManager.LoadScene("TelaCarregamento");
    }
}
