using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSlice : MonoBehaviour
{
     public Text scoreText;
    public NPCDumpsterDiving npc;

    void Start()
    {

    }

    void Update()
    {
        scoreText.text = "Score : " + npc.score.ToString();
        if (npc.score >= 50)
            npc.CorrectAnswer();
        else if(npc.score < 0)
            npc.WrongAnswer();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Doc1")
        {
            Destroy(target.gameObject);
            npc.ScoreCount(true, 1);
        }
        else if(target.tag == "Doc2")
        {
            Destroy(target.gameObject);
            npc.ScoreCount(true, 2);
        }
        else if(target.tag == "Doc3")
        {
            Destroy(target.gameObject);
            npc.ScoreCount(true, 3);
        }
        else if(target.tag == "Doc4")
        {
            Destroy(target.gameObject);
            npc.ScoreCount(true, 4);
        }
    }
    void OnTriggerExit2D(Collider2D target)
    {

    }
}
