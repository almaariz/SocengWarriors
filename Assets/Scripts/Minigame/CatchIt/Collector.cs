using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public NPCTailgating npc;
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Officer")
        {
            npc.ScoreCount(false);
            Destroy(target.gameObject);
            AudioManager.i.PlaySfx(AudioManager.AudioId.Dump);
        }
        else if (target.tag == "Tailgater")
        {
            Destroy(target.gameObject);
            AudioManager.i.PlaySfx(AudioManager.AudioId.Slice);
        }
    }
}
