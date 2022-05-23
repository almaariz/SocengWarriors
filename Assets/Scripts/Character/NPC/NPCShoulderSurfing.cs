using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCShoulderSurfing : NPCController
{
  public GameObject canvas;
  bool isPlaying = false;
  bool isAnswered = false;
  [SerializeField] Text pinText;

  // Start is called before the first frame update
  void Update()
  {
      
  }

  public void WrongAnswer(bool enter)
  {
    isPlaying = false;
    isAnswered = true;
    List<string> lines = new List<string>();
    if (enter)
    {
      lines.Add("Tidaaak");
      lines.Add("Pin atm ku hilang");
    }
    else
    {
      lines.Add("Ayolah");
      lines.Add("Aku ga akan nyuri");
    }

    dialog.setLines(lines);
    canvas.SetActive(false);
    StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
    {
      idleTimer = 0f;
      state = NPCState.Idle;
      isAnswered = false;
      List<string> lines = new List<string>();

      if (enter)
      {
      lines.Add("Lebih hati-hati bre");
      lines.Add("Ayo ke atm lagi");
      }
      else
      {
        lines.Add("Ayo kau masih ingatkan?");
        lines.Add("177013");
      }
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
      lines.Add("Saldo alhamdulillah aman");
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
            GameController.Instance.PlayingGame(true);
            isPlaying = true;
            canvas.SetActive(true);
          }
          idleTimer = 0f;
          state = NPCState.Idle;
        }));
      }
    }

  }

  public void Button(int i)
  {
      pinText.text +=  i.ToString();
  }

  public void ClearText()
  {
    pinText.text = "";
  }
}
