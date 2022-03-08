using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class PlayerController : MonoBehaviour 
{ 
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
                StartCoroutine(character.Move(input));
            } 
        } 

        character.HandleUpdate();

        if (Input.GetKeyDown(KeyCode.Z)) 
            Interact(); 
    }
 
    void Interact() 
    { 
        var facingDir = new Vector3(character.aanimator.MoveX, character.aanimator.MoveY); 
        var interactPos = transform.position + facingDir; 
 
        //Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f); 
 
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.Instance.InteractableLayer); 
        if (collider != null) 
        { 
            collider.GetComponent<Interactable>()?.Interact(transform); 
        } 
    } 
}