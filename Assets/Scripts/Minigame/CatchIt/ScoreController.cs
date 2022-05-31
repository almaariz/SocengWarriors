using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    public NPCTailgating npc;

    void Start()
    {

    }

    void Update()
    {
        scoreText.text = "Score : " + npc.score.ToString();
        if (npc.score == 30)
            npc.CorrectAnswer();
        else if(npc.score < 0)
            npc.WrongAnswer(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Tailgater")
        {
            Destroy(target.gameObject);
            npc.WrongAnswer(true);
        }
        if(target.tag == "Officer")
        {
            Destroy(target.gameObject);
            npc.ScoreCount(true);
            AudioManager.i.PlaySfx(AudioManager.AudioId.Dump);
        }
    }
    void OnTriggerExit2D(Collider2D target)
    {
        
    }
}
