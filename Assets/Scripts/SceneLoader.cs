using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneLoader : MonoBehaviour
{
    public GameObject loaderUI;
    public Slider progressSlider;
    public Animator transition;
    public static Scene MainMenu;
    public static string[] scenes;  
    public static int currentSceneIndex = 0;  

    // Добавьте поле для видеоклипа
    public VideoPlayer videoPlayer;
    public RawImage videoImage;

    public void Start()
    {
        scenes = new string[] { "Menu", "CutScene", "SampleScene"};  
        StartCoroutine(C_LoadScene());
    }

    public IEnumerator C_LoadScene()
    {
        progressSlider.value = 0;
        loaderUI.SetActive(true);
        transition.SetTrigger("Start");

        // Запуск видеоклипа, если он есть
        if (videoPlayer != null)
        {
            // Убедитесь, что режим рендеринга видеоплеера установлен на "Render Texture"
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;

            // Запуск воспроизведения видео
            videoPlayer.Play();

            // Отобразите видео на RawImage
            videoImage.texture = videoPlayer.targetTexture;
            videoImage.gameObject.SetActive(true);
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scenes[currentSceneIndex]); //Тут поменять сцену на нужную
        asyncOperation.allowSceneActivation = false;
        float progress = 0;

        while(!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress,asyncOperation.progress, Time.deltaTime);
            progressSlider.value = progress;
            if(progress >= 0.9f)
            {
                progressSlider.value = 1;
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }

        // Остановка видео, если оно играло
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            videoImage.gameObject.SetActive(false);
        }
    }
}