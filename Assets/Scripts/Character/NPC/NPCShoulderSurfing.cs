using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCShoulderSurfing : NPCController
{
  public GameObject canvas;
  bool isPlaying = false;
  // bool isAnswered = false;
  [SerializeField] Text pinText;
  int isDone = 0;
  

  public void WrongAnswer(bool enter)
  {
    AudioManager.i.PlaySfx(AudioManager.AudioId.WrongAnswer, pauseMusic:true);
    isPlaying = false;
    GameController.Instance.badgeStatus["badge2"] = true;
    List<string> lines = new List<string>();
    if (enter)
    {
      lines.Add("Tidaaak");
      lines.Add("Pin atm ku dicuri");
      dialog.setLines(lines);
    }
    else
    {
      lines.Add("Waduh");
      lines.Add("Tidak mau ya");
      dialog.setLines(lines);
    }

    canvas.SetActive(false);
    StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

    idleTimer = 0f;
    state = NPCState.Idle;
    GameController.Instance.badgeStatus["badge2"] = false;

    if (enter)
    {
      isDone = 1;
    }
    else
    {
      isDone = 2;
    }
    ClearText();
  }

  public void CorrectAnswer()
  {
    AudioManager.i.PlaySfx(AudioManager.AudioId.CorrectAnswer, pauseMusic:true);
    isPlaying = false;
    GameController.Instance.badgeStatus["badge2"] = true;
    List<string> lines = new List<string>();
    lines.Add("Benar sekali");
    lines.Add("Orang lain jadi tidak bisa melihat apa yang kita masukkan");

    // GameController.Instance.miniGameDone += 1;

    dialog.setLines(lines);
    canvas.SetActive(false);

    StartCoroutine(DialogManager.Instance.ShowDialog(dialog,getBadge:true));

    idleTimer = 0f;
    state = NPCState.Idle;
    isDone = 3;

    GameController.Instance.badgeStatus["badge2"] = true;
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
        if (!GameController.Instance.badgeStatus["badge2"])
        {
          GameController.Instance.PlayingGame(true);
          isPlaying = true;
          canvas.SetActive(true);
        }
        idleTimer = 0f;
        state = NPCState.Idle;
      }
    }
    else
      yield return null;
  }

  public void Button(int i)
  {
      pinText.text +=  i.ToString();
  }

  public void ClearText()
  {
    pinText.text = "";
  }
  public void CheckDialog(int done)
  {
    var x = Random.Range(0, 1000000);
    string s = x.ToString("000000");
    List<string> lines = new List<string>();
    if(done == 1)
    {
      lines.Add("Lebih hati-hati lagi bre");
      lines.Add("Ini pin baruku " + s);
      dialog.setLines(lines);
    }
    else if (done == 2)
    {
      lines.Add("Ayo kau masih ingatkan?");
      lines.Add(s);
      dialog.setLines(lines);
    }
    else if (done == 3 || GameController.Instance.badgeStatus["badge2"])
    {
      lines.Add("Terima kasih");
      lines.Add("Berkatmu saldoku aman");
      dialog.setLines(lines);
    }
  }
}
