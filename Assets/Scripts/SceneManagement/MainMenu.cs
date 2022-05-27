using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Fader fader;

    void Start()
    {
        fader = FindObjectOfType<Fader>();
    }
    public void PlayGame()
    {
        // fader.FadeIn(0.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // fader.FadeOut(0.5f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
