using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpostorController : MonoBehaviour
{
    [SerializeField] GameObject fov;
    [SerializeField] protected Dialog dialog;
    protected NPCState state;
    protected float idleTimer = 0f;
    protected Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        SetFovRotation(character.aanimator.DefaultDirection);
    }

    private void Update()
    {
        CheckStatus();
    }

    public IEnumerator Interact(Transform initiator)
    {
        character.LookTowards(initiator.position);

        yield return DialogManager.Instance.ShowDialog(dialog);
    }

    public IEnumerator TriggerCall(PlayerController player)
    {
        var diff = player.transform.position - transform.position;
        var moveVec = diff - diff.normalized;
        moveVec = new Vector2(Mathf.Round(moveVec.x), Mathf.Round(moveVec.y));

        yield return character.Move(moveVec);

        yield return DialogManager.Instance.ShowDialog(dialog);
        idleTimer = 0f;
        state = NPCState.Idle;

        GameController.Instance.DTheftDone=2;
        GameController.Instance.DTheftStatus=false;
        
        yield return character.Move(-1*moveVec);
        
        character.aanimator.SetFacingDirection(character.aanimator.DefaultDirection);
    }
    
    public void CheckStatus()
    {
        if(GameController.Instance.DTheftStatus)
        {
            fov.SetActive(true);
            GameController.Instance.DTheftDone=2;
        }
        else
            fov.SetActive(false);
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
