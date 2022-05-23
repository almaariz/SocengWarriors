using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentSpawner : MonoBehaviour
{
    public GameObject[] doc;

    public float xMin, xMax, yBound;

    void Start()
    {
        StartCoroutine(SpawnRandomObject());
    }

    IEnumerator SpawnRandomObject()
    {
        yield return new WaitForSeconds(Random.Range(1,2));

        int randomOfficer = Random.Range(0, doc.Length);

        if (Random.value <= .6f)
        {
            GameObject newOfficer = Instantiate(doc[randomOfficer], new Vector2(Random.Range(xMin, xMax),yBound), Quaternion.identity) as GameObject;
            newOfficer.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
        }
        else
        {
            GameObject newOfficer = Instantiate(doc[randomOfficer], new Vector2(Random.Range(xMin, xMax),yBound), Quaternion.identity) as GameObject;
            newOfficer.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
        }

        StartCoroutine(SpawnRandomObject());
    }
}
