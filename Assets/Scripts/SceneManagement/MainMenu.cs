using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public Animator transitionAnim;
    public int sceneIndex;
    public AudioManager audioManager;
    public AudioClip audioClip;
    // [SerializeField] AudioSource musicPlayer;

    void Start()
    {
    //     musicPlayer.clip = audioClip;
    //     musicPlayer.loop = true;
    //     musicPlayer.Play();
        AudioManager.i.PlayMusic(audioClip);
    }
    public void PlayGame()
    {
        // fader.FadeIn(0.5f);
        AudioManager.i.stopMusic(audioClip);
        AudioManager.i.PlaySfx(AudioManager.AudioId.StartGame);

        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadScene());

        // fader.FadeOut(0.5f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(sceneBuildIndex:sceneIndex);
    }
}
