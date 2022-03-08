using System;
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
 
public class DialogueManager : MonoBehaviour 
{ 
    [SerializeField] GameObject dialogueBox; 
    [SerializeField] Text dialogueText; 
    [SerializeField] int letterPerSecond; 
 
    public event Action onShowDialogue; 
    public event Action onCloseDialogue; 
 
    public static DialogueManager Instance { get; private set; } 
 
    private void Awake(){ 
        Instance = this; 
    } 
 
    Dialogue dialogue; 
    int currentLine = 0; 
    bool isTyping; 

    public bool IsShowing { get; private set;}
 
    public IEnumerator ShowDialog(Dialogue dialogue) 
    { 
        yield return new WaitForEndOfFrame(); 
 
        onShowDialogue?.Invoke(); 
 
        IsShowing = true;
        this.dialogue = dialogue; 
        dialogueBox.SetActive(true); 
        StartCoroutine(TypeDialogue(dialogue.Lines[0])); 
    } 
 
    public void HandleUpdate() 
    { 
        if(Input.GetKeyDown(KeyCode.Z) && !isTyping) 
        { 
            ++currentLine; 
            if (currentLine < dialogue.Lines.Count) 
            { 
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine])); 
            } 
            else 
            { 
                currentLine = 0; 
                IsShowing = false;
                dialogueBox.SetActive(false); 
                onCloseDialogue?.Invoke(); 
            } 
        } 
    } 
 
    public IEnumerator TypeDialogue(string line) 
    { 
        isTyping = true; 
        dialogueText.text = ""; 
        foreach (var letter in line.ToCharArray()) 
        { 
            dialogueText.text += letter; 
            yield return new WaitForSeconds(1f / letterPerSecond); 
        } 
        isTyping = false; 
    } 
}