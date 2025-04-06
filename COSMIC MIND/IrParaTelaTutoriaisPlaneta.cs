using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IrParaTelaTutoriaisPlaneta : MonoBehaviour
{
   


    public Button VoltarTelaInicio;

    void Start()
    {
        VoltarTelaInicio.onClick.AddListener(VoltarTela);
    }

   


    void VoltarTela()
    {
        SceneManager.LoadScene("TelaPlanetaTutoriais");
    }

}
