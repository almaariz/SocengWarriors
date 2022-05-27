using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPhising : NPCController
{
  public GameObject canvas;
  bool isPlaying = false;
  bool isAnswered = false;
  int isDone = 0;

  public void WrongAnswer()
  {
    isPlaying = false;
    isAnswered = true;
    List<string> lines = new List<string>();
    lines.Add("Wah ternyata itu hanya penipuan");
    lines.Add("Huhuhu");

    dialog.setLines(lines);
    canvas.SetActive(false);

    StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

    idleTimer = 0f;
    state = NPCState.Idle;
    isAnswered = false;
    isDone = 1;
  }

  public void CorrectAnswer()
  {
    isPlaying = false;
    isAnswered = true;
    List<string> lines = new List<string>();
    lines.Add("Wah mantap");
    lines.Add("Jadi dengan mengabaikan pesan tersebut kita bisa terhindar dari serangan phising ya");

    GameController.Instance.miniGameDone += 1;

    dialog.setLines(lines);
    canvas.SetActive(false);
    StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    idleTimer = 0f;
    state = NPCState.Idle;
    isDone = 2;
    GameController.Instance.badge1status = true;
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
        if (!isAnswered)
        {
          GameController.Instance.PlayingGame(true);
          isPlaying = true;
          canvas.SetActive(true);
        }
        idleTimer = 0f;
        state = NPCState.Idle;
        yield break;
      }
    }
  }
  public void CheckDialog(int done)
  {
    List<string> lines = new List<string>();
    if(done == 1)
    {
      lines.Add("Bagaimana ini");
      lines.Add("Apa yang harus kulakukan dengan ini??");
      dialog.setLines(lines);
    }
    else if (done == 2)
    {
      lines.Add("Terima kasih");
      lines.Add("Berkatmu aku tidak tertipu dengan serangan phising");
      dialog.setLines(lines);
    }
  }
}