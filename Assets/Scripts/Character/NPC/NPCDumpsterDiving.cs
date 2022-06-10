using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDumpsterDiving : NPCController
{
  public GameObject canvas;
  
  public GameObject spawnerPrefab;
  GameObject spawner;
  bool isPlaying = false;
  // bool isAnswered = false;
  public int score;
  int isDone = 0;

  public void WrongAnswer()
  {
    AudioManager.i.PlaySfx(AudioManager.AudioId.WrongAnswer, pauseMusic:true);
    isPlaying = false;
    GameController.Instance.badgeStatus["badge3"] = true;
    List<string> lines = new List<string>();

    lines.Add("Wah, dataku ternyata dicuri orang");
    lines.Add("Waduh gimana nih");
    dialog.setLines(lines);

    canvas.SetActive(false);
    StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    
    idleTimer = 0f;
    state = NPCState.Idle;
    GameController.Instance.badgeStatus["badge3"] = false;
    isDone = 1;
    Destroy(spawner);
    score = 0;
  }

  public void CorrectAnswer()
  {
    AudioManager.i.PlaySfx(AudioManager.AudioId.CorrectAnswer, pauseMusic:true);
    isPlaying = false;
    GameController.Instance.badgeStatus["badge3"] = true;
    List<string> lines = new List<string>();
    lines.Add("Oh aku harus hancurkan dokumenku terlebih dahulu ya");
    lines.Add("Biar orang gabisa ngambil informasi dari dokumen itu");

    // GameController.Instance.miniGameDone += 1;

    dialog.setLines(lines);
    canvas.SetActive(false);

    StartCoroutine(DialogManager.Instance.ShowDialog(dialog,getBadge:true));

    idleTimer = 0f;
    state = NPCState.Idle;
    isDone = 2;

    GameController.Instance.badgeStatus["badge3"] = true;
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

        if (!GameController.Instance.badgeStatus["badge3"])
        {
          GameController.Instance.PlayingGame(true);
          isPlaying = true;
          canvas.SetActive(true);
          spawner = Instantiate(spawnerPrefab, new Vector2(0,0), Quaternion.identity);
          spawner.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
        }
        idleTimer = 0f;
        state = NPCState.Idle;
      }
    }
  }
  public void ScoreCount(bool plus, int doc)
  {
    if(plus)
    {
      if(doc == 1)
        score+=4;
      else if (doc == 2)
        score+=3;
      else if (doc == 3)
        score+=1;
      else if (doc == 4)
        score-=1;
    }
    else
      if(doc == 1)
        score-=8;
      else if (doc == 2)
        score-=6;
      else if (doc == 3)
        score-=1;
      else if (doc == 4)
        score+=0;
  }

  public void CheckDialog(int done)
  {
    List<string> lines = new List<string>();
    if(done == 1)
    {
      lines.Add("Bantu aku untuk membuang sampah ini");
      lines.Add("Ayo");
      dialog.setLines(lines);
    }
    else if (done == 2)
    {
      lines.Add("Terima kasih banyak");
      lines.Add("Berkatmu aku bisa terhindar dari serangan dumpster diving");
      dialog.setLines(lines);
    }
  }
}
