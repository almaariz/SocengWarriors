using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpostorController : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    [SerializeField] GameObject fov;
    Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        SetFovRotation(character.aanimator.DefaultDirection);
        CheckStatus();
    }

    private void Update()
    {
        character.HandleUpdate();
    }

    public IEnumerator TriggerCall(PlayerController player)
    {
        var diff = player.transform.position - transform.position;
        var moveVec = diff - diff.normalized;
        moveVec = new Vector2(Mathf.Round(moveVec.x), Mathf.Round(moveVec.y));

        yield return character.Move(moveVec);

        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
        {
            
        }));
        GameController.Instance.DTheftDone=2;
        GameController.Instance.DTheftStatus=false;
        Debug.Log("Paket sampai salah");
    }
    
    public void CheckStatus()
    {
        if(GameController.Instance.DTheftStatus==true)
        {
            GameController.Instance.DTheftDone=2;
        }
    }
    public void SetFovRotation(FacingDirection dir)
    {
        float angle = 0f;
        if (dir == FacingDirection.Right)
            angle = 90f;
        else if (dir == FacingDirection.Up)
            angle = 180f;
        else if (dir == FacingDirection.Left)
            angle = 270f;

        fov.transform.eulerAngles = new Vector3(0f, 0f, angle);
    }
}
