using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VoltarTutoriais : MonoBehaviour
{
    public Button VoltarTutoriais1; 

    void Start()
    {
        if (VoltarTutoriais1 != null)
        {
            VoltarTutoriais1.onClick.AddListener(IrVoltarTutoriais1);
        }
        else
        {
            Debug.LogError("O campo Fase1Guia não foi atribuído no Inspector!");
        }
    }

    void IrVoltarTutoriais1()
    {
        SceneManager.LoadScene("TutoriaisTelaInicial");
    }
}

