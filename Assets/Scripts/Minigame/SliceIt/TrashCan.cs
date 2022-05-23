using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public NPCDumpsterDiving npc;
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Doc1")
        {
            npc.ScoreCount(false, 1);
            Destroy(target.gameObject);
        }
        else if (target.tag == "Doc2")
        {
            npc.ScoreCount(false, 2);
            Destroy(target.gameObject);
        }
        else if (target.tag == "Doc3")
        {
            npc.ScoreCount(false, 3);
            Destroy(target.gameObject);
        }
        else if (target.tag == "Doc4")
        {
            npc.ScoreCount(false, 4);
            Destroy(target.gameObject);
        }
    }
}
