using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuidProQuo : NPCController
{
    public GameObject canvas;
    bool isPlaying = false;
    bool isAnswered = false;
    public int score;
    public bool isSigned;

    public void WrongAnswer()
    {
        isPlaying = false;
        isAnswered = true;
        List<string> lines = new List<string>();
        lines.Add("Wah pegawai banyak yang komplen karena dicegat");
        lines.Add("Payah");

        dialog.setLines(lines);
        canvas.SetActive(false);
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
        {
        idleTimer = 0f;
        state = NPCState.Idle;
        isAnswered = false;
        List<string> lines = new List<string>();
        lines.Add("Jaga lagi lah kuy");
        lines.Add("Gas");
        dialog.setLines(lines);
        }));
        score = 0;
    }

    public void CorrectAnswer()
    {
        isPlaying = false;
        isAnswered = true;
        List<string> lines = new List<string>();
        lines.Add("Widih mantap");
        lines.Add("Ga ada orang luar masuk");

        GameController.Instance.miniGameDone += 1;

        dialog.setLines(lines);
        canvas.SetActive(false);
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
        {
        idleTimer = 0f;
        state = NPCState.Idle;
        List<string> lines = new List<string>();
        lines.Add("Tengkyu bre");
        lines.Add("kantor aman");
        dialog.setLines(lines);
        }));
        GameController.Instance.badge3status = true;
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
                isSigned = false;
                canvas.SetActive(true);
                }
                idleTimer = 0f;
                state = NPCState.Idle;
            }));
            }
        }
    }
}
