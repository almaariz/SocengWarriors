using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPretexting : NPCController
{
  public GameObject canvas;
  bool isPlaying = false;
  bool isAnswered = false;

  // Start is called before the first frame update
  void Update()
  {
      
  }

  public void WrongAnswer()
  {
    isPlaying = false;
    isAnswered = true;
    List<string> lines = new List<string>();
    lines.Add("Orang yang salah");
    lines.Add("Penipu lolos");

    dialog.setLines(lines);
    canvas.SetActive(false);
    StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
    {
      idleTimer = 0f;
      state = NPCState.Idle;
      isAnswered = false;
      List<string> lines = new List<string>();
      lines.Add("Belom nyerah??");
      lines.Add("Ayo cari lagi");
      dialog.setLines(lines);
    }));
  }

  public void CorrectAnswer()
  {
    isPlaying = false;
    isAnswered = true;
    List<string> lines = new List<string>();
    lines.Add("Widih bener");
    lines.Add("Mantap");

    GameController.Instance.miniGameDone += 1;

    dialog.setLines(lines);
    canvas.SetActive(false);
    StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
    {
      idleTimer = 0f;
      state = NPCState.Idle;
      List<string> lines = new List<string>();
      lines.Add("Tengkyu bre");
      lines.Add("Udah ketangkep si penipu");
      dialog.setLines(lines);
    }));
  }

  public override void Interact(Transform initiator)
  {
    if (!isPlaying)
    {
      if (state == NPCState.Idle)
      {
        state = NPCState.Dialog;
        character.LookTowards(initiator.position);

        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
        {
          if (!isAnswered)
          {
            GameController.Instance.PauseGame(true);
            isPlaying = true;
            canvas.SetActive(true);
          }
          idleTimer = 0f;
          state = NPCState.Idle;
        }));
      }
    }

  }
}
