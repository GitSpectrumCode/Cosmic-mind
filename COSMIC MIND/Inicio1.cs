using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inicio1 : MonoBehaviour
{
    [SerializeField] Button btnCadastrar;
    [SerializeField] Button btnLogin;
    void Start()
    {
        btnCadastrar.onClick.AddListener(IrParaTelaCadastrar);
        btnLogin.onClick.AddListener(IrParaTelaEntrar);

    }

   void IrParaTelaCadastrar()
    {
        SceneManager.LoadScene("TelaCadastrar");
    }
    void IrParaTelaEntrar()
    {
        SceneManager.LoadScene("TelaLogin");
    }
   
}
