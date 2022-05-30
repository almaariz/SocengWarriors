using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    public float speed = 1f;
    float i;
    public bool title = true;

    [HideInInspector]public Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        if(title)
        {
            i++;
            float newPosition = Mathf.Repeat(Time.time * speed, i);
            transform.position = startPosition + Vector3.right * newPosition;
        }
        else
        {
            i =0;
            float newPosition = Mathf.Repeat(Time.time * speed, i);
            transform.position = startPosition;
        }
    }
    public void SetPlay()
    {
        title = false;
    }
}
