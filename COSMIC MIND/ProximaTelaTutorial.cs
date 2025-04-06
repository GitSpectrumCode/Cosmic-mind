using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProximaTelaTutorial : MonoBehaviour
{
    public Button continueButton;

    void Start()
    {
        continueButton.onClick.AddListener(OnContinueButtonClicked);
    }

    void OnContinueButtonClicked()
    {
        SceneManager.LoadScene("TutorialQuebraCabeca");
    }
}
