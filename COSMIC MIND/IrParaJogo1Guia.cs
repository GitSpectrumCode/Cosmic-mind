using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class IrParaJogo1Guia : MonoBehaviour
{
    public Button Fase1Guia; 

    void Start()
    {
        if (Fase1Guia != null)
        {
            Fase1Guia.onClick.AddListener(IrFase1);
        }
        else
        {
            Debug.LogError("O campo Fase1Guia não foi atribuído no Inspector!");
        }
    }

    void IrFase1()
    {
        SceneManager.LoadScene("TelaPlanetaTutoriais");
    }
}
