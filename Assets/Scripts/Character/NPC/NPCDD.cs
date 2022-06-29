using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDD : NPCController
{

  public override IEnumerator Interact(Transform initiator)
  {
    if (state == NPCState.Idle)
    {
      state = NPCState.Dialog;
      yield return DialogManager.Instance.ShowDialog(dialog);
      idleTimer = 0f;
      state = NPCState.Idle;
      yield break;
    }
  }
}