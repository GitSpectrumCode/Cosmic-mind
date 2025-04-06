using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Configuracao : MonoBehaviour
{
    public Slider sliderMusica;
    public Slider sliderEfeitoSonoro;
    public Button botaoMusica;
    public Button botaoEfeitoSonoro;

    public Sprite somAtivado;
    public Sprite somDesativado;

    public Sprite efeitoAtivado;
    public Sprite efeitoDesativado;

    public Text mensagemStatus;
    public Button botaoSalvar;
    public Button botaoVoltar;

    public AudioSource audioMusica;
    public AudioSource audioEfeitos;

    private bool musicaAtivada = true;
    private bool efeitosAtivados = true;
    private bool configuracaoModificada = false;

    void Start()
    {
       
        mensagemStatus.gameObject.SetActive(false);

        CarregarConfiguracoes();

    
        sliderMusica.value = audioMusica.volume;
        sliderEfeitoSonoro.value = audioEfeitos.volume;

       
        sliderMusica.onValueChanged.AddListener(delegate { AtualizarVolumeMusica(); });
        sliderEfeitoSonoro.onValueChanged.AddListener(delegate { AtualizarVolumeEfeitos(); });

        botaoMusica.onClick.AddListener(TrocarEstadoMusica);
        botaoEfeitoSonoro.onClick.AddListener(TrocarEstadoEfeitos);

        botaoSalvar.onClick.AddListener(SalvarConfiguracoes);
        botaoVoltar.onClick.AddListener(VoltarParaMenu);


        AtualizarSpriteBotaoMusica();
        AtualizarSpriteBotaoEfeitos();
    }

    void CarregarConfiguracoes()
    {
        
        if (PlayerPrefs.HasKey("volumeMusica"))
        {
            audioMusica.volume = PlayerPrefs.GetFloat("volumeMusica");
        }
        if (PlayerPrefs.HasKey("volumeEfeitos"))
        {
            audioEfeitos.volume = PlayerPrefs.GetFloat("volumeEfeitos");
        }

        musicaAtivada = PlayerPrefs.GetInt("musicaAtivada", 1) == 1;
        efeitosAtivados = PlayerPrefs.GetInt("efeitosAtivados", 1) == 1;

      
        audioMusica.mute = !musicaAtivada;
        audioEfeitos.mute = !efeitosAtivados;
    }

    void AtualizarVolumeMusica()
    {
        audioMusica.volume = sliderMusica.value;
        configuracaoModificada = true;
    }

    void AtualizarVolumeEfeitos()
    {
        audioEfeitos.volume = sliderEfeitoSonoro.value;
        configuracaoModificada = true;
    }

    void TrocarEstadoMusica()
    {
        musicaAtivada = !musicaAtivada;
        audioMusica.mute = !musicaAtivada;
        sliderMusica.value = musicaAtivada ? audioMusica.volume : 0;    
        AtualizarSpriteBotaoMusica();
        configuracaoModificada = true;
    }

    void TrocarEstadoEfeitos()
    {
        efeitosAtivados = !efeitosAtivados;
        audioEfeitos.mute = !efeitosAtivados;
        sliderEfeitoSonoro.value = efeitosAtivados ? audioEfeitos.volume : 0;  
        AtualizarSpriteBotaoEfeitos();
        configuracaoModificada = true;
    }

    void AtualizarSpriteBotaoMusica()
    {
        botaoMusica.image.sprite = musicaAtivada ? somAtivado : somDesativado;
    }

    void AtualizarSpriteBotaoEfeitos()
    {
        botaoEfeitoSonoro.image.sprite = efeitosAtivados ? efeitoAtivado : efeitoDesativado;
    }

    IEnumerator ExibirMensagem(string mensagem, bool sucesso)
    {
       
        mensagemStatus.text = mensagem;
        mensagemStatus.color = sucesso ? Color.green : Color.red;
        mensagemStatus.gameObject.SetActive(true);

      
        botaoSalvar.interactable = false;
        botaoVoltar.interactable = false;

       
        yield return new WaitForSeconds(2);

        mensagemStatus.gameObject.SetActive(false);
        botaoSalvar.interactable = true;
        botaoVoltar.interactable = true;
    }

    void SalvarConfiguracoes()
    {
        PlayerPrefs.SetFloat("volumeMusica", sliderMusica.value);
        PlayerPrefs.SetFloat("volumeEfeitos", sliderEfeitoSonoro.value);
        PlayerPrefs.SetInt("musicaAtivada", musicaAtivada ? 1 : 0);
        PlayerPrefs.SetInt("efeitosAtivados", efeitosAtivados ? 1 : 0);

        configuracaoModificada = false;
        StartCoroutine(ExibirMensagem("Configurações salvas com sucesso!", true));
    }

    void VoltarParaMenu()
    {
        if (configuracaoModificada)
        {
            StartCoroutine(ExibirMensagem("Configurações não salvas! Clique em salvar para aplicar.", false));
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TelaMenu");
        }
    }
}
