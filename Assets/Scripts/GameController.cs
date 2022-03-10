using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialog, Cutscene, Paused }

public class GameController : MonoBehaviour
{ 
    [SerializeField] PlayerController playerController;
 
    GameState state; 

    GameState stateBeforePause;

    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
 
    private void Start() 
    { 
        DialogManager.Instance.onShowDialog += () => 
        { 
            state = GameState.Dialog; 
        }; 

        DialogManager.Instance.onCloseDialog += () => 
        { 
            if(state == GameState.Dialog)
                state = GameState.FreeRoam; 
        }; 
    } 
 
    public void PauseGame(bool pause)
    {
        if (pause)
        {
            stateBeforePause = state;
            state = GameState.Paused;
        }
        else
        {
            state = stateBeforePause;
        }
    }

    private void Update() 
    { 
        if (state == GameState.FreeRoam) 
        { 
            playerController.HandleUpdate();
        } 
        else if (state == GameState.Dialog) 
        { 
            DialogManager.Instance.HandleUpdate(); 
        } 
    } 

    public void OnImpostorView(ImpostorController impostor)
    {
        state = GameState.Cutscene;
        StartCoroutine(impostor.TriggerCall(playerController));
    }
}