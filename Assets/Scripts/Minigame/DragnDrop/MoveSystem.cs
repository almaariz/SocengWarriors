using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveSystem : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler {
    private Vector2 lastMousePosition;
    private float x, y, z;
    public GameObject correctForm;
    public NPCShoulderSurfing npc;

    public bool blockX, blockY, blockZ;

    public int jarak;

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
        float distance = Vector3.Distance(transform.localPosition, correctForm.transform.localPosition);
        if (distance < jarak)
        {
            transform.localPosition = correctForm.transform.localPosition;
            npc.CorrectAnswer();
            Debug.Log("Manggil correct answer");
        }
        else
            transform.localPosition = orginalPosition;
    }
}
