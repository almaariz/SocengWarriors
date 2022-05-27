using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : NPCController
{
    // public IEnumerator Heal(Transform player, Dialog dialog)
    // {
    //     int selectedChoice = 0;

    //     yield return DialogManager.Instance.ShowDialog(dialog,
    //     choices: new List<string> { "Yes", "No" },
    //     onChoiceSelected: (choiceIndex) => selectedChoice = choiceIndex);

    //     if (selectedChoice == 0)
    //     {
    //         Debug.Log("Yes");
    //         yield return DialogManager.Instance.ShowDialogText($"Okay");
    //     }
    //     else if (selectedChoice == 1)
    //     {
    //         Debug.Log("No");
    //     }
    // }

    public override IEnumerator Interact(Transform initiator)
    {
        int selectedChoice = 0;
        if (state == NPCState.Idle)
        {
            state = NPCState.Dialog;
            character.LookTowards(initiator.position);

            yield return DialogManager.Instance.ShowDialogText("Ayo pilih yang mana?",
            choices: new List<string> { "Yes", "No" },
            onChoiceSelected: (choiceIndex) => selectedChoice = choiceIndex);

            if (selectedChoice == 0)
            {
                Debug.Log("Yes");
                yield return DialogManager.Instance.ShowDialogText($"Okay");
            }
            else if (selectedChoice == 1)
            {
                Debug.Log("No");
            }
        }
    }
}
