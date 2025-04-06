using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorTutorial2 : MonoBehaviour
{
    public GameObject tutorialPanel1;  // Referência ao painel do tutorial

    void Start()
    {
        // Certifique-se de que o painel esteja visível no início
        tutorialPanel1.SetActive(true);
    }

    void Update()
    {
        // Verifica se o usuário clicou em qualquer lugar da tela
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
