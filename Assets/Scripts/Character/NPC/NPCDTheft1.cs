using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDTheft1 : NPCController
{
    public GameObject canvas;
    bool isPlaying = false;
    // bool isAnswered = false;
    int isDone = 0;

    public void CheckStatus()
    {
        GameController.Instance.PlayingGame(false);
        if (GameController.Instance.DTheftDone == 0)
        {
            isPlaying = false;
            canvas.SetActive(false);
            GameController.Instance.DTheftStatus=true;
            AudioManager.i.PlaySfx(AudioManager.AudioId.PlayGame, pauseMusic:true);
        }
        else if (GameController.Instance.DTheftDone == 1)
        {
            CorrectAnswer();
            AudioManager.i.PlaySfx(AudioManager.AudioId.CorrectAnswer, pauseMusic:true);
        }
        else if (GameController.Instance.DTheftDone == 2)
        {
            WrongAnswer();
            AudioManager.i.PlaySfx(AudioManager.AudioId.WrongAnswer, pauseMusic:true);
        }
    }

    public void WrongAnswer()
    {
    isPlaying = false;
    GameController.Instance.badgeStatus["badge4"] = true;
    List<string> lines = new List<string>();
    lines.Add("*diam... memalingkan muka*");
    lines.Add("*kecewa karena paket tidak sampai*");

    dialog.setLines(lines);
    canvas.SetActive(false);

    StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    idleTimer = 0f;
    state = NPCState.Idle;
    GameController.Instance.badgeStatus["badge4"] = false;
    isDone = 1;

    GameController.Instance.DTheftDone=0;
    }

    public void CorrectAnswer()
    {
    isPlaying = false;
    GameController.Instance.badgeStatus["badge4"] = true;
    List<string> lines = new List<string>();
    lines.Add("Mantap, dengan memastikan orang yang mendapatkan barang");
    lines.Add("Kita bisa terhindar dari serangan diversion theft");

    // GameController.Instance.miniGameDone += 1;

    dialog.setLines(lines);
    canvas.SetActive(false);
    StartCoroutine(DialogManager.Instance.ShowDialog(dialog,getBadge:true));
    idleTimer = 0f;
    state = NPCState.Idle;
    isDone = 2;

    GameController.Instance.badgeStatus["badge4"] = true;
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
                
                if (!GameController.Instance.badgeStatus["badge4"])
                {
                    GameController.Instance.PlayingGame(true);
                    isPlaying = true;
                    canvas.SetActive(true);
                    AudioManager.i.PlaySfx(AudioManager.AudioId.UISelect, pauseMusic:true);
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
        else if (done == 2 || GameController.Instance.badgeStatus["badge4"])
        {
        lines.Add("Terima kasih");
        lines.Add("Selalu berhati-hati saat mengirim ataupun menerima barang");
        dialog.setLines(lines);
        
        }
    }
}
