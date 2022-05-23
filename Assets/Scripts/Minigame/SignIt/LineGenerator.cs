using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    public GameObject linePrefab;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    Lines activeLine;
    public float minCuttingVelocity = .001f;

	bool isCutting = false;

	Vector2 previousPosition;
    GameObject newLine;

    void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		circleCollider = GetComponent<CircleCollider2D>();
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
		{
			StartSign();

		} else if (Input.GetMouseButtonUp(0))
		{
			StopCutting();
		}

		if (activeLine !=null)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);

		}
        if (isCutting)
		{
			UpdateCut();
		}
    }
    
    void UpdateCut ()
	{
		Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;

		previousPosition = newPosition;
	}
    void StartSign ()
	{
		isCutting = true;
		newLine = Instantiate(linePrefab);
        activeLine = newLine.GetComponent<Lines>();
        activeLine.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform,false);
		previousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		circleCollider.enabled = false;
	}

    void StopCutting ()
	{
		isCutting = false;
		activeLine = null;
        circleCollider.enabled = true;
	}

    public void DestroyLine()
    {
        Destroy(newLine);
    }
}
