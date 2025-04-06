using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IrTutorialBuraco : MonoBehaviour
{
    public Button Fase1BuracoNegro; 

    void Start()
    {
        if (Fase1BuracoNegro != null)
        {
            Fase1BuracoNegro.onClick.AddListener(IrFaseBuraco1);
        }
        else
        {
            Debug.LogError("O campo Fase1Guia não foi atribuído no Inspector!");
        }
    }

    void IrFaseBuraco1()
    {
        SceneManager.LoadScene("TutorialQuebraCabecaTutorial");
    }
}
