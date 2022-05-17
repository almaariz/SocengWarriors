using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DumpsterDocument : MonoBehaviour
{
    [SerializeField] Vector2 targetPosition;
    [SerializeField] int moveSpeed;

    void Awake()
    {
        var position = new Vector3(Random.Range(600.0f, 1800.0f), 1200, transform.position.z);
        transform.position = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(Move(targetPosition));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(gameObject.name + " Collided");
        if (collider.tag == "Blade")
        {
            Debug.Log("cut");
            Destroy(gameObject);
        }
        else if (collider.tag == "TrashCan")
        {
            Debug.Log("trashed");
            Destroy(gameObject);
        }
    }

    // public void OnMouseEnter()
    // {
    //     Debug.Log("cut");
    //     Destroy(gameObject);
    // }

    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     print("OnMouseEnter");
    // }
}
