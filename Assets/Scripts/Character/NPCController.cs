using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class NPCController : MonoBehaviour, Interactable 
{ 
 
    [SerializeField] Dialogue dialogue;
    [SerializeField] List<Vector2> movementPattern;
    [SerializeField] float timeBetweenPattern;
    NPCState state;
    float idleTimer = 0f;
    int currentPattern = 0;
    // [SerializeField] List<Sprite> sprites;
    Character character;

    // SpriteAnimator spriteAnimator;
 
    private void Awake()
    {
        character = GetComponent<Character>();
    }

    // private void Start()
    // {
    //     spriteAnimator = new SpriteAnimator(sprites, GetComponent<SpriteRenderer>());
    //     spriteAnimator.Start();
    // }

    // private void Update()
    // {
    //     spriteAnimator.HandleUpdate();
    // }

    public void Interact() 
    { 
        StartCoroutine(DialogueManager.Instance.ShowDialog(dialogue));
    } 

    private void Update()
    {
        if(DialogueManager.Instance.IsShowing) return;

        if (state == NPCState.Idle)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer > timeBetweenPattern)
            {
                idleTimer = 0f;
                if(movementPattern.Count > 0)
                    StartCoroutine(Walk());
            }
        }
        character.HandleUpdate();
    }

    IEnumerator Walk()
    {
        state = NPCState.Walking;
    
        yield return character.Move(movementPattern[currentPattern]);
        currentPattern = (currentPattern + 1) % movementPattern.Count;

        state = NPCState.Idle;
    }
}

public enum NPCState { Idle, Walking }