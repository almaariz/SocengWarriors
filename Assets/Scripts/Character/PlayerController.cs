using System;
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class PlayerController : MonoBehaviour, ISavable
{ 
    [SerializeField] string pname;
    [SerializeField] Sprite sprite;
    private Vector2 input; 
    private Character character;
    bool keyAButton;
 
    private void Awake() 
    { 
        character = GetComponent<Character>();
    } 
 
    public void HandleUpdate() 
    { 
        if (!character.IsMoving) 
        { 
            input.x = SimpleInput.GetAxisRaw("Horizontal"); 
            input.y = SimpleInput.GetAxisRaw("Vertical"); 
 
            //remove diagonal movement 
            if (input.x != 0) input.y = 0; 
 
            if (input != Vector2.zero) 
            {  
                StartCoroutine(character.Move(input, OnMoveOver));
            } 
        } 

        character.Update();

        // keyAButton = Input.GetKeyDown(KeyCode.Space);
        if (keyAButton)
            StartCoroutine(Interact()); 
    }
 
    public IEnumerator Interact() 
    { 
        var facingDir = new Vector3(character.aanimator.MoveX, character.aanimator.MoveY); 
        var interactPos = transform.position + facingDir; 
 
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.Instance.InteractableLayer); 
        if (collider != null) 
        { 
            yield return collider.GetComponent<Interactable>()?.Interact(transform); 
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

    public object CaptureState()
    {
        float[] position = new float[] {transform.position.x, transform.position.y};
        return position;
    }

    public void RestoreState(object state)
    {
        var position = (float[])state;
        transform.position = new Vector3(position[0], position[1]);
    }

    public string Pname {
        get => Pname;
    }

    public Sprite Sprite {
        get => sprite;
    }

    public Character Character => character;

    public void MobileAWrapper()
    {
        StartCoroutine(ButtonA());
    }

    private IEnumerator ButtonA()
    {
        keyAButton = true;
        yield return new WaitForSeconds(0.01f);
        keyAButton = false;
    }
    
}