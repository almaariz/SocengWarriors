using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { FreeRoam, Dialog, Cutscene, Paused }

public class GameController : MonoBehaviour
{
  [SerializeField] PlayerController playerController;

  public int miniGameDone { get; set; }

  // [SerializeField] Text miniGameDonerText;
  public bool badge1status, badge2status, badge3status, badge4status, badge5status, badge6status, badge7status;
  public GameObject badge1, badge2, badge3, badge4, badge5, badge6, badge7;
  public int DTheftDone { get; set; }
  public bool DTheftStatus { get; set; }

  GameState state;

  GameState stateBeforePause;

  public SceneDetails CurrentScene { get; private set; }
  public SceneDetails PrevScene { get; private set; }

  public static GameController Instance { get; private set; }

  public GameObject canvas, backButton, pauseButton, endingScene;

  public AudioClip winningMusic;
  public Animator transitionAnim;
  public int sceneIndex;

  public float waitTime = 5f;


  private void Awake()
  {
    Instance = this;
  }

  private void Start()
  {
    DialogManager.Instance.onShowDialog += () =>
    {
      state = GameState.Dialog;
    };

    DialogManager.Instance.onCloseDialog += () =>
    {
      if (state == GameState.Dialog)
        state = GameState.FreeRoam;
    };
  }

  public void PauseGame(bool pause)
  {
    if (pause)
    {
      stateBeforePause = state;
      state = GameState.Paused;
      canvas.SetActive(true);
      pauseButton.SetActive(false);
      backButton.SetActive(true);
      // text.SetActive(false);
    }
    else
    {
      canvas.SetActive(false);
      pauseButton.SetActive(true);
      backButton.SetActive(false);
      // text.SetActive(true);
      state = stateBeforePause;
    }
  }
  public void PlayingGame(bool play)
  {
    if (play)
    {
      state = GameState.Paused;
    }
    else
    {
      state = GameState.FreeRoam;
    }
  }

  private void Update()
  {
    if (state == GameState.FreeRoam)
    {
      playerController.HandleUpdate();
      
      if (Input.GetKeyDown(KeyCode.S))
      {
        SavingSystem.i.Save("saveSlot1");
      }
      if (Input.GetKeyDown(KeyCode.L))
      {
        SavingSystem.i.Load("saveSlot1");
      }
    }
    else if (state == GameState.Dialog)
    {
      DialogManager.Instance.HandleUpdate();
    }
    if (miniGameDone == 7)
    {
      AudioManager.i.PlayMusic(winningMusic, fade: true);
      StartCoroutine(LoadScene());
    }

    // miniGameDonerText.text = "Mini Games Completed = " + miniGameDone;
    CheckBadgeStatus();
  }

  public void SetCurrentScene(SceneDetails currScene)
  {
    PrevScene = CurrentScene;
    CurrentScene = currScene;
  }

  public void OnImpostorView(ImpostorController impostor)
  {
    state = GameState.Cutscene;
    StartCoroutine(impostor.TriggerCall(playerController));
  }

  void CheckBadgeStatus()
  {
    if (badge1status)
      badge1.SetActive(true);
    if(badge2status)
      badge2.SetActive(true);
    if(badge3status)
      badge3.SetActive(true);
    if(badge4status)
      badge4.SetActive(true);
    if(badge5status)
      badge5.SetActive(true);
    if(badge6status)
      badge6.SetActive(true);
    if(badge7status)
      badge7.SetActive(true);
  }
  IEnumerator LoadScene()
    {
        if(waitTime == 0)
      {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
        yield break;
      }
      else
      {
        state = GameState.Paused;
        yield return new WaitForSeconds(waitTime);
        // AudioManager.i.PlaySfx(AudioManager.AudioId.GameEnd);
        endingScene.SetActive(true);
        yield break;
      }
    }

    public void SkipCredit(){
      waitTime = 0f;
    }
}