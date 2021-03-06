using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{

  [SerializeField] protected Dialog dialog;
  [SerializeField] List<Vector2> movementPattern;
  [SerializeField] float timeBetweenPattern;
  protected NPCState state;
  protected float idleTimer = 0f;
  int currentPattern = 0;
  // [SerializeField] List<Sprite> sprites;
  protected Character character;

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

  public virtual IEnumerator Interact(Transform initiator)
  {
    if (state == NPCState.Idle)
    {
      state = NPCState.Dialog;
      character.LookTowards(initiator.position);

      yield return DialogManager.Instance.ShowDialog(dialog);

      idleTimer = 0f;
      state = NPCState.Idle;
    }
  }

  private void Update()
  {
    if (DialogManager.Instance.IsShowing) return;

    if (state == NPCState.Idle)
    {
        idleTimer += Time.deltaTime;
        if (idleTimer > timeBetweenPattern)
        {
          idleTimer = 0f;
          if (movementPattern.Count > 0)
            StartCoroutine(Walk());
        }
    }
    character.Update();
  }

  IEnumerator Walk()
  {
    state = NPCState.Walking;

    var oldPos = transform.position;

    yield return character.Move(movementPattern[currentPattern]);

    if (transform.position != oldPos)
      currentPattern = (currentPattern + 1) % movementPattern.Count;

    state = NPCState.Idle;
  }
}

public enum NPCState { Idle, Walking, Dialog }