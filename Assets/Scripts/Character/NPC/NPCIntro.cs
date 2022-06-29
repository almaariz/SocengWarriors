using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIntro : NPCController
{
  int isDone = 0;
  // bool isPlaying = false;

  public override IEnumerator Interact(Transform initiator)
  {
    CheckDialog(isDone);
    // if (!isPlaying)
    // {
      if (state == NPCState.Idle)
      {
        state = NPCState.Dialog;
        character.LookTowards(initiator.position);
        yield return DialogManager.Instance.ShowDialog(dialog);
        idleTimer = 0f;
        state = NPCState.Idle;
        isDone = 1;
        yield break;
      }
    // }
  }
  public void CheckDialog(int done)
  {
    List<string> lines = new List<string>();
    if(done == 1)
    {
      lines.Add("Terdapat 7 minigame di setiap tempat di dunia ini. Kau bisa melihatnya di tombol pause");
      lines.Add("Kumpulkan semua badge dan bantu semua orang");
      lines.Add("Jika kau kesulitan untuk menyelesaikannya. Carilah orang yang memiliki clue untuk game tersebut");
      dialog.setLines(lines);
    }
  }
}