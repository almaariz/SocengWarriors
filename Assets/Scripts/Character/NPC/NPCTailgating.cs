using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTailgating : NPCController
{
    public GameObject canvas;
    public GameObject spawnerPrefab;
    GameObject spawner;
    bool isPlaying = false;
    bool isAnswered = false;
    public int score;
    int isDone = 0;

    public void WrongAnswer(bool tailgater)
    {
        AudioManager.i.PlaySfx(AudioManager.AudioId.WrongAnswer, pauseMusic:true);
        isPlaying = false;
        isAnswered = true;
        List<string> lines = new List<string>();

        if(tailgater)
        {
            lines.Add("Wah, kamu biarin orang luar masuk");
            lines.Add("Payah");
            dialog.setLines(lines);
        }
        else
        {
            lines.Add("Wah pegawai banyak yang komplen karena dicegat");
            lines.Add("Payah");
            dialog.setLines(lines);
        }

        canvas.SetActive(false);

        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

        idleTimer = 0f;
        state = NPCState.Idle;
        isAnswered = false;
        isDone = 1;

        Destroy(spawner);
        score = 0;
    }

    public void CorrectAnswer()
    {
        AudioManager.i.PlaySfx(AudioManager.AudioId.CorrectAnswer, pauseMusic:true);
        isPlaying = false;
        isAnswered = true;
        List<string> lines = new List<string>();
        lines.Add("Widih mantap");
        lines.Add("Ga ada orang luar masuk");

        GameController.Instance.miniGameDone += 1;

        dialog.setLines(lines);
        canvas.SetActive(false);

        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));

        idleTimer = 0f;
        state = NPCState.Idle;
        isDone = 2;

        GameController.Instance.badge7status = true;
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
                canvas.SetActive(true);
                spawner = Instantiate(spawnerPrefab, new Vector2(0,0), Quaternion.identity);
                spawner.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
            }
            idleTimer = 0f;
            state = NPCState.Idle;
        }
        }
    }

    public void ScoreCount(bool plus)
    {
        if(plus)
            score++;
        else
            score--;
    }
    public void CheckDialog(int done)
    {
        List<string> lines = new List<string>();
        if(done == 1)
        {
            lines.Add("Ayo bantu aku jaga lagi");
            lines.Add("C'mon");
            dialog.setLines(lines);
        }
        else if (done == 2)
        {
            lines.Add("Terima kasih banyak");
            lines.Add("Kita harus berhati-hati terhadap orang luar yang mencoba masuk");
            dialog.setLines(lines);
        }
    }
}
