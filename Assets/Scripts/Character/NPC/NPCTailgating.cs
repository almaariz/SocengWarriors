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

    public void WrongAnswer(bool tailgater)
    {
        isPlaying = false;
        isAnswered = true;
        List<string> lines = new List<string>();

        if(tailgater)
        {
            lines.Add("Wah kamu biarin orang luar masuk");
            lines.Add("Payah");
        }
        else
        {
            lines.Add("Wah pegawai banyak yang komplen karena dicegat");
            lines.Add("Payah");
        }

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
        Destroy(spawner);
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
                GameController.Instance.PauseGame(true);
                isPlaying = true;
                canvas.SetActive(true);
                spawner = Instantiate(spawnerPrefab, new Vector2(0,0), Quaternion.identity);
                spawner.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
            }
            idleTimer = 0f;
            state = NPCState.Idle;
            }));
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
}
