using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DocumentSign : MonoBehaviour
{
    public NPCQuidProQuo npc;
    public LineGenerator LG;

    void Awake()
    {
        // var position = new Vector3(Random.Range(600.0f, 1800.0f), 1200, transform.position.z);
        // transform.position = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(Move(targetPosition));
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Blade")
        {
            npc.isSigned = true;
            LG.sign = true;
            DestroyWithTag ("Line");
        }
    }
    void OnTriggerExit2D(Collider2D target)
    {

    }
    void CheckStatus()
    {
        if(npc.isSigned)
            npc.WrongAnswer();
    }
    void DestroyWithTag (string destroyTag)
    {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject)
            Destroy (oneObject);
    }
}
