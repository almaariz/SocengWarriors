using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoorMovement : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    private Vector2 lastMousePosition;
    private float x, y, z;

    public bool blockX, blockY, blockZ;


    private Vector3 orginalPosition;

    private void Start() {
        orginalPosition = transform.localPosition;
    }


    public void OnBeginDrag(PointerEventData eventData) {
        lastMousePosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData) {
        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - lastMousePosition;

        RectTransform rectTransform = GetComponent<RectTransform>();

        x = y = z = 0;

        if (!blockX)
            x = diff.x;

        if (!blockY)
            y = diff.y;

        if (!blockZ)
            z = transform.localPosition.z;

        rectTransform.position = rectTransform.position + new Vector3(x, y, z);

        lastMousePosition = currentMousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        
    }
}
