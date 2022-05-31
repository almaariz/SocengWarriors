using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDTheft2 : NPCController
{
    int isDone = 0;
    public override IEnumerator Interact(Transform initiator)
    {
        CheckDialog(isDone);
        if(GameController.Instance.DTheftStatus)
        {
            if (state == NPCState.Idle)
            {
                state = NPCState.Dialog;
                character.LookTowards(initiator.position);
                
                List<string> lines = new List<string>();
                lines.Add("Oh pengantar barang dari Alex, terima kasih");
                lines.Add("Tolong sampaikan salamku ya");
                dialog.setLines(lines);

                yield return DialogManager.Instance.ShowDialog(dialog);

                idleTimer = 0f;
                state = NPCState.Idle;
                isDone = 1;

                GameController.Instance.DTheftDone = 1;
                GameController.Instance.DTheftStatus = false;
                AudioManager.i.PlaySfx(AudioManager.AudioId.Complete);
            }
        }
        else
        {
            if (state == NPCState.Idle)
            {
            state = NPCState.Dialog;
            character.LookTowards(initiator.position);

            yield return DialogManager.Instance.ShowDialog(dialog);
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
            lines.Add("Hello");
            lines.Add("Namaku Bob");
            dialog.setLines(lines);
        }
    }
}
