using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDTheft1 : NPCController
{
    public GameObject canvas;
    bool isPlaying = false;
    bool isAnswered = false;
    int isDone = 0;

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
    lines.Add("*kecewa karena paket tidak sampai*");

    dialog.setLines(lines);
    canvas.SetActive(false);

    StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    idleTimer = 0f;
    state = NPCState.Idle;
    isAnswered = false;
    isDone = 1;

    GameController.Instance.DTheftDone=0;
    }

    public void CorrectAnswer()
    {
    isPlaying = false;
    isAnswered = true;
    List<string> lines = new List<string>();
    lines.Add("Mantap, dengan memastikan orang yang mendapatkan barang");
    lines.Add("Kita bisa terhindar dari serangan diversion theft");

    GameController.Instance.miniGameDone += 1;

    dialog.setLines(lines);
    canvas.SetActive(false);
    StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    idleTimer = 0f;
    state = NPCState.Idle;
    isDone = 2;

    GameController.Instance.badge4status = true;
    }

    public override IEnumerator Interact(Transform initiator)
    {
        CheckDialog(isDone);
        if(!GameController.Instance.DTheftStatus)
        {
            if (!isPlaying)
            {
                if (state == NPCState.Idle)
                {
                state = NPCState.Dialog;
                character.LookTowards(initiator.position);

                yield return DialogManager.Instance.ShowDialog(dialog);
                
                if (!isAnswered)
                {
                    GameController.Instance.PlayingGame(true);
                    isPlaying = true;
                    canvas.SetActive(true);
                }
                idleTimer = 0f;
                state = NPCState.Idle;                
                }
            }
        }
    }
    public void CheckDialog(int done)
    {
        List<string> lines = new List<string>();
        if(done == 1)
        {
        lines.Add("*isyarat tangan*");
        lines.Add("*memberikan kertas*");
        dialog.setLines(lines);
        }
        else if (done == 2)
        {
        lines.Add("Terima kasih");
        lines.Add("Selalu berhati-hati saat mengirim ataupun menerima barang");
        dialog.setLines(lines);
        }
    }
}
