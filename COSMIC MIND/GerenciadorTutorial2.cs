using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorTutorial2 : MonoBehaviour
{
    public GameObject tutorialPanel1;  // Refer�ncia ao painel do tutorial

    void Start()
    {
        // Certifique-se de que o painel esteja vis�vel no in�cio
        tutorialPanel1.SetActive(true);
    }

    void Update()
    {
        // Verifica se o usu�rio clicou em qualquer lugar da tela
        if (Input.GetMouseButtonDown(0))
        {
            CloseTutorial();
        }
    }

    void CloseTutorial()
    {
        tutorialPanel1.SetActive(false);
    }
}
