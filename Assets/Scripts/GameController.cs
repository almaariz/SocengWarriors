using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { FreeRoam, Dialog, Cutscene, Paused }

public class GameController : MonoBehaviour
{
  [SerializeField] PlayerController playerController;

  public int miniGameDone { get; set; }

  [SerializeField] Text miniGameDonerText;
  public int DTheftDone { get; set; }
  public bool DTheftStatus { get; set; }

  GameState state;

  GameState stateBeforePause;

  public SceneDetails CurrentScene { get; private set; }
  public SceneDetails PrevScene { get; private set; }

  public static GameController Instance { get; private set; }

  public GameObject canvas, backButton, pauseButton, text;

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
      text.SetActive(false);
    }
    else
    {
      canvas.SetActive(false);
      pauseButton.SetActive(true);
      backButton.SetActive(false);
      text.SetActive(true);
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
    }
    else if (state == GameState.Dialog)
    {
      DialogManager.Instance.HandleUpdate();
    }
    miniGameDonerText.text = "Mini Games Completed = " + miniGameDone;
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
}