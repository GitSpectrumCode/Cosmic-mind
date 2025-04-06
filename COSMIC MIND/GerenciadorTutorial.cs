using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorTutorial : MonoBehaviour
{
    public GameObject tutorialPanel;  

    void Start()
    {
       
        tutorialPanel.SetActive(true);
    }

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            CloseTutorial();
        }
    }

    void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}
