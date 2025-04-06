using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class IntroVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; 
    public Button skipButton; 
  
   

    void Start()
    {
       

        skipButton.onClick.AddListener(SkipVideo);
       

        videoPlayer.Play();

     
        videoPlayer.loopPointReached += OnVideoFinished;

    
     
       
    }

    void SkipVideo()
    {
        videoPlayer.Stop();
        StartNarracao();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        StartNarracao();
    }

    void StartNarracao()
    {
      
        videoPlayer.gameObject.SetActive(false); 
        skipButton.gameObject.SetActive(false);
         
        
    }

    void BackToVideo()
    {
        videoPlayer.gameObject.SetActive(true); 

        skipButton.gameObject.SetActive(true);
   
        videoPlayer.Play();
    }
}
