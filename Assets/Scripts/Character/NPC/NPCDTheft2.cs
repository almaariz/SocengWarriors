using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDTheft2 : NPCController
{
    void Update()
    {
        
    }
    public override void Interact(Transform initiator)
    {
        if(GameController.Instance.DTheftStatus)
        {
            if (state == NPCState.Idle)
            {
            state = NPCState.Dialog;
            character.LookTowards(initiator.position);
            
            List<string> lines = new List<string>();
            lines.Add("Oh pengantar barang dari Alex");
            lines.Add("Sangkyu bor");
            dialog.setLines(lines);

            StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
            {
                idleTimer = 0f;
                state = NPCState.Idle;
                List<string> lines = new List<string>();
                lines.Add("Hello");
                lines.Add("Namaku Bob");
                dialog.setLines(lines);
            }));
            }
            GameController.Instance.DTheftDone = 1;
            GameController.Instance.DTheftStatus = false;
        }
        else
        {
            if (state == NPCState.Idle)
            {
            state = NPCState.Dialog;
            character.LookTowards(initiator.position);

            StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
            {
                idleTimer = 0f;
                state = NPCState.Idle;
            }));
            }
        }
    }
}
