using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuidProQuo : NPCController
{
    public GameObject canvas, canvas2;
    bool isPlaying = false;
    bool isAnswered = false;
    public bool isSigned;
    int isDone = 0;
    int noSign = 0;

    public void WrongAnswer()
    {
        AudioManager.i.PlaySfx(AudioManager.AudioId.CorrectAnswer, pauseMusic:true);
        isPlaying = false;
        isAnswered = true;
        
        List<string> lines = new List<string>();
        lines.Add("Oke, terima kasih");
        lines.Add("Wahaha...");
        dialog.setLines(lines);
        

        canvas2.SetActive(false);
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
        idleTimer = 0f;
        state = NPCState.Idle;
        isAnswered = false;
        isDone = 1;
    }

    public void CorrectAnswer()
    {
        AudioManager.i.PlaySfx(AudioManager.AudioId.Complete, pauseMusic:true);
        isPlaying = false;
        isAnswered = true;
        List<string> lines = new List<string>();
        lines.Add("Aku suka keteguhanmu, jangan sampai kau memberikan informasi untuk sesuatu yang tidak pasti");
        lines.Add("Dengan tidak mudah percaya, kamu bisa terhindar dari serangan quid pro quo");

        GameController.Instance.miniGameDone += 1;

        dialog.setLines(lines);
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog,getBadge:true));
        idleTimer = 0f;
        state = NPCState.Idle;
        isDone = 2;
        GameController.Instance.badge5status = true;
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

            if (!isAnswered)
            {
            GameController.Instance.PlayingGame(true);
            isPlaying = true;
            isSigned = false;
            canvas.SetActive(true);
            }
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
            lines.Add("Ohiya aku lupa, tolong tanda tangani lagi");
            lines.Add("Nanti aku kasih badge");
            dialog.setLines(lines);
        }
        else if (done == 2)
        {
            lines.Add("Selamat untuk badgemu");
            lines.Add("Selalu berhati-hati dan jangan mudah percaya");
            dialog.setLines(lines);
        }
        else if(done == 3)
        {
            lines.Add("Ini kesempatanmu sekarang");
            lines.Add("Yakin gamau?");
            dialog.setLines(lines);
        }
    }

    public void signYes()
    {
        AudioManager.i.PlaySfx(AudioManager.AudioId.PlayGame, pauseMusic:true);
        canvas2.SetActive(true);
        canvas.SetActive(false);
    }

    public void signNo()
    {
        if(noSign < 5)
        {
            AudioManager.i.PlaySfx(AudioManager.AudioId.WrongAnswer, pauseMusic:true);
            canvas.SetActive(false);
            isPlaying = false;
            isAnswered = true;
            List<string> lines = new List<string>();
            lines.Add("Kalau tidak mau tidak apa apa");
            lines.Add("Jangan menyesal ya");
            dialog.setLines(lines);
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

            canvas.SetActive(false);
            idleTimer = 0f;
            state = NPCState.Idle;
            isAnswered = false;
            isDone = 3;
            noSign++;
        }
        else
        {
            canvas.SetActive(false);
            CorrectAnswer();
        }
    }
}
