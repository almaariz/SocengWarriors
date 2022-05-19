using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] officer;
    public GameObject tailgater;

    public float xBound, yBound;

    void Start()
    {
        StartCoroutine(SpawnRandomObject());
    }

    IEnumerator SpawnRandomObject()
    {
        yield return new WaitForSeconds(Random.Range(1,2));

        int randomOfficer = Random.Range(0, officer.Length);

        if (Random.value <= .6f)
        {
            GameObject newOfficer = Instantiate(officer[randomOfficer], new Vector2(Random.Range(-xBound, xBound),yBound), Quaternion.identity) as GameObject;
            newOfficer.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
        }
        else
        {
            GameObject newTailgater = Instantiate(tailgater, new Vector2(Random.Range(-xBound, xBound),yBound), Quaternion.identity) as GameObject;
            newTailgater.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
        }

        StartCoroutine(SpawnRandomObject());
    }
}
