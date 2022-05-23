using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDTheft1 : NPCController
{
    public GameObject canvas;
    bool isPlaying = false;
    bool isAnswered = false;

  // Start is called before the first frame update
    void Update()
    {
        
    }

    public void CheckStatus()
    {
        GameController.Instance.PlayingGame(false);
        if (GameController.Instance.DTheftDone == 0)
        {
            isPlaying = false;
            canvas.SetActive(false);
            GameController.Instance.DTheftStatus=true;
        }
        else if (GameController.Instance.DTheftDone == 1)
        {
            CorrectAnswer();
        }
        else if (GameController.Instance.DTheftDone == 2)
        {
            WrongAnswer();
        }
    }

    public void WrongAnswer()
    {
    isPlaying = false;
    isAnswered = true;
    List<string> lines = new List<string>();
    lines.Add("*diam... memalingkan muka*");
    lines.Add("*sepertinya salah orang, mungkin pastikan lagi paket sampai*");

    dialog.setLines(lines);
    canvas.SetActive(false);
    StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
    {
        idleTimer = 0f;
        state = NPCState.Idle;
        isAnswered = false;
        List<string> lines = new List<string>();
        lines.Add("*isyarat tangan*");
        lines.Add("*memberikan kertas*");
        dialog.setLines(lines);
    }));
    GameController.Instance.DTheftDone=0;
    }

    public void CorrectAnswer()
    {
    isPlaying = false;
    isAnswered = true;
    List<string> lines = new List<string>();
    lines.Add("Widih dah nyampe barangnya");
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
        lines.Add("Barang aman sampai tujuan");
        dialog.setLines(lines);
    }));
    GameController.Instance.badge4status = true;
    }

    public override void Interact(Transform initiator)
    {
        if(!GameController.Instance.DTheftStatus)
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
    }
}
