using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPretexting : NPCController
{
  public GameObject canvas;
  bool isPlaying = false;
  // bool isAnswered = false;
  int isDone = 0;

  public void WrongAnswer()
  {
    AudioManager.i.PlaySfx(AudioManager.AudioId.WrongAnswer, pauseMusic:true);
    isPlaying = false;
    GameController.Instance.badgeStatus["badge6"] = true;
    List<string> lines = new List<string>();
    lines.Add("Ternyata bukan temanku");
    lines.Add("Bagaimana ini");

    dialog.setLines(lines);
    canvas.SetActive(false);
    StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    idleTimer = 0f;
    state = NPCState.Idle;
    GameController.Instance.badgeStatus["badge6"] = false;
    isDone = 1;
  }

  public void CorrectAnswer()
  {
    AudioManager.i.PlaySfx(AudioManager.AudioId.CorrectAnswer, pauseMusic:true);
    isPlaying = false;
    GameController.Instance.badgeStatus["badge6"] = true;
    List<string> lines = new List<string>();
    lines.Add("Wah, kau benar");
    lines.Add("Dia ternyata penipu");

    // GameController.Instance.miniGameDone += 1;

    dialog.setLines(lines);
    canvas.SetActive(false);

    StartCoroutine(DialogManager.Instance.ShowDialog(dialog,getBadge:true));

    idleTimer = 0f;
    state = NPCState.Idle;
    isDone = 2;
      
    GameController.Instance.badgeStatus["badge6"] = true;
  }

  public override IEnumerator Interact(Transform initiator)
  {
    CheckDialog(isDone);
    if (!isPlaying)
    {
      if (state == NPCState.Idle)
      {
        state = NPCState.Dialog;
        character.LookTowards(initiator.position);

        yield return DialogManager.Instance.ShowDialog(dialog);

        if (!GameController.Instance.badgeStatus["badge6"])
        {
          GameController.Instance.PlayingGame(true);
          isPlaying = true;
          canvas.SetActive(true);
        }
        idleTimer = 0f;
        state = NPCState.Idle;
      }
    }
  }
  public void CheckDialog(int done)
  {
    List<string> lines = new List<string>();
    if(done == 1)
    {
      lines.Add("Ada yang meneleponku lagi");
      lines.Add("Aku tidak yakin dia temanku");
      dialog.setLines(lines);
    }
    else if (done == 2 || GameController.Instance.badgeStatus["badge6"])
    {
      lines.Add("Terima kasih");
      lines.Add("Aku akan berhati-hati jika ada yang menelepon lagi");
      dialog.setLines(lines);
    }
  }
}
