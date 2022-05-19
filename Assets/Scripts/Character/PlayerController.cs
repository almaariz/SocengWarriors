using System;
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class PlayerController : MonoBehaviour 
{ 
    [SerializeField] string pname;
    [SerializeField] Sprite sprite;
    private Vector2 input; 
    private Character character;
 
    private void Awake() 
    { 
        character = GetComponent<Character>();
    } 
 
    public void HandleUpdate() 
    { 
        if (!character.IsMoving) 
        { 
            input.x = Input.GetAxisRaw("Horizontal"); 
            input.y = Input.GetAxisRaw("Vertical"); 
 
            //remove diagonal movement 
            if (input.x != 0) input.y = 0; 
 
            if (input != Vector2.zero) 
            {  
                StartCoroutine(character.Move(input, OnMoveOver));
            } 
        } 

        character.Update();

        if (Input.GetKeyDown(KeyCode.Space)) 
            Interact(); 
    }
 
    void Interact() 
    { 
        var facingDir = new Vector3(character.aanimator.MoveX, character.aanimator.MoveY); 
        var interactPos = transform.position + facingDir; 
 
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.Instance.InteractableLayer); 
        if (collider != null) 
        { 
            collider.GetComponent<Interactable>()?.Interact(transform); 
        } 
    } 

    private void OnMoveOver()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, character.OffsetY), 0.2f, GameLayers.Instance.TriggerableLayers);

        foreach (var collider in colliders)
        {
            var triggerable = collider.GetComponent<IPlayerTriggerable>();
            if (triggerable != null)
            {
                triggerable.OnPlayerTriggered(this);
                break;
            }
        }
    }

    public string Pname {
        get => Pname;
    }

    public Sprite Sprite {
        get => sprite;
    }

    public Character Character => character;
}