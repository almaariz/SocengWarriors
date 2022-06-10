using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { FreeRoam, Dialog, Cutscene, Paused }

public class GameController : MonoBehaviour, ISavable
{
  [SerializeField] PlayerController playerController;

  public int miniGameDone { get; set; }

  // [SerializeField] Text miniGameDonerText;
  // public bool badge1status, badge2status, badge3status, badge4status, badge5status, badge6status, badge7status;
  public GameObject badge1, badge2, badge3, badge4, badge5, badge6, badge7;
  public GameObject loc1, loc2, loc3, loc4, loc5, loc6, loc7, locintro, lochall;
  int m1, m2, m3, m4, m5, m6, m7;

  public Dictionary<string, bool> badgeStatus = new Dictionary<string, bool>();

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
    // LoadState();
  }

  private void Start()
  {
    badgeStatus.Add("badge1", false);
    badgeStatus.Add("badge2", false);
    badgeStatus.Add("badge3", false);
    badgeStatus.Add("badge4", false);
    badgeStatus.Add("badge5", false);
    badgeStatus.Add("badge6", false);
    badgeStatus.Add("badge7", false);
    LoadState();

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
      
      if (Input.GetKeyDown(KeyCode.G))
      {
        SavingSystem.i.Save("saveSlot1");
      }
      if (Input.GetKeyDown(KeyCode.H))
      {
        SavingSystem.i.Load("saveSlot1");
      }
    }
    else if (state == GameState.Dialog)
    {
      DialogManager.Instance.HandleUpdate();
      if (!DialogManager.Instance.IsShowing)
      {
        state=GameState.FreeRoam;
      }
    }
    miniGameDone = m1+m2+m3+m4+m5+m6+m7;
    if (miniGameDone == 7)
    {
      AudioManager.i.PlayMusic(winningMusic, fade: true);
      StartCoroutine(LoadScene());
    }

    // miniGameDonerText.text = "currLine = " + DialogManager.Instance.currentLine + " currDialog length = " + DialogManager.Instance.dialogLine;
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
    if (badgeStatus["badge1"])
    {
      badge1.SetActive(true);
      m1 = 1;
    }
    // else
    //   badge1.SetActive(false);
    if(badgeStatus["badge2"])
    {
      badge2.SetActive(true);
      m2 = 1;
    }
    // else
    //   badge2.SetActive(false);
    if(badgeStatus["badge3"])
    {
      badge3.SetActive(true);
      m3 = 1;
    }
    // else
    //   badge3.SetActive(false);
    if(badgeStatus["badge4"])
    {
      badge4.SetActive(true);
      m4 = 1;
    }
    // else
    //   badge4.SetActive(false);
    if(badgeStatus["badge5"])
    {
      badge5.SetActive(true);
      m5 = 1;
    }
    // else
    //   badge5.SetActive(false);
    if(badgeStatus["badge6"])
    {
      badge6.SetActive(true);
      m6 = 1;
    }
    // else
    //   badge6.SetActive(false);
    if(badgeStatus["badge7"])
    {
      badge7.SetActive(true);
      m7 = 1;
    }
    // else
    //   badge7.SetActive(false);
  }
  IEnumerator LoadScene()
    {
        if(waitTime == 0)
      {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SavingSystem.i.Delete("saveSlot1");
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

  public void SaveState()
  {
    SavingSystem.i.Save("saveSlot1");
    // state = GameState.FreeRoam;
    StartCoroutine(LoadMainScene());
    
  }
  IEnumerator LoadMainScene()
  {
    transitionAnim.SetTrigger("end");
    yield return new WaitForSeconds(1.5f);
    SceneManager.LoadSceneAsync(sceneBuildIndex:sceneIndex);
    // Application.Quit();
  }

  public void LoadState()
  {
    SavingSystem.i.Load("saveSlot1");
  }

    public object CaptureState()
    {
        bool[] savedStatus = new bool[] {badgeStatus["badge1"], badgeStatus["badge2"], badgeStatus["badge3"], badgeStatus["badge4"], badgeStatus["badge5"], badgeStatus["badge6"], badgeStatus["badge7"]};
        return savedStatus;
    }

    public void RestoreState(object state)
    {
        var savedStatus = (bool[])state;
        badgeStatus["badge1"] = savedStatus[0];
        badgeStatus["badge2"] = savedStatus[1];
        badgeStatus["badge3"] = savedStatus[2];
        badgeStatus["badge4"] = savedStatus[3];
        badgeStatus["badge5"] = savedStatus[4];
        badgeStatus["badge6"] = savedStatus[5];
        badgeStatus["badge7"] = savedStatus[6];
    }
}