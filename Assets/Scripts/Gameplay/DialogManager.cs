using System;
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
 
public class DialogManager : MonoBehaviour 
{ 
    [SerializeField] GameObject dialogBox; 
    [SerializeField] Text dialogText; 
    [SerializeField] int letterPerSecond; 
 
    public event Action onShowDialog; 
    public event Action onCloseDialog; 
 
    public static DialogManager Instance { get; private set; } 
 
    private void Awake(){ 
        Instance = this; 
    } 
 
    Dialog dialog; 
    Action onDialogFinished;
    int currentLine = 0; 
    bool isTyping; 

    public bool IsShowing { get; private set;}
 
    public IEnumerator ShowDialog(Dialog dialog, Action onFinished=null) 
    { 
        yield return new WaitForEndOfFrame(); 
 
        onShowDialog?.Invoke(); 
 
        IsShowing = true;
        this.dialog = dialog; 
        onDialogFinished = onFinished;

        dialogBox.SetActive(true); 
        StartCoroutine(TypeDialog(dialog.Lines[0])); 
    } 
 
    public void HandleUpdate() 
    { 
        if(Input.GetKeyDown(KeyCode.Space) && !isTyping) 
        { 
            ++currentLine; 
            if (currentLine < dialog.Lines.Count) 
            { 
                StartCoroutine(TypeDialog(dialog.Lines[currentLine])); 
            } 
            else 
            { 
                currentLine = 0; 
                IsShowing = false;
                dialogBox.SetActive(false); 
                onDialogFinished?.Invoke();
                onCloseDialog?.Invoke(); 
            } 
        } 
    } 
 
    public IEnumerator TypeDialog(string line) 
    { 
        isTyping = true; 
        dialogText.text = ""; 
        foreach (var letter in line.ToCharArray()) 
        { 
            dialogText.text += letter; 
            yield return new WaitForSeconds(1f / letterPerSecond); 
        } 
        isTyping = false; 
    } 
}