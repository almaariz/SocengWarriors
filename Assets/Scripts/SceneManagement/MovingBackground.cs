using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    public Rigidbody2D rb;
    public float initSpeed = 1f;

    void Start()
    {
        rb.AddForce(Vector3.right * initSpeed);
    }
}
