using System;
using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
 
public class DialogManager : MonoBehaviour 
{ 
    [SerializeField] GameObject dialogBox; 
    [SerializeField] ChoiceBox choiceBox;
    [SerializeField] Text dialogText; 
    [SerializeField] int letterPerSecond; 
    bool keyAButton;
 
    public event Action onShowDialog; 
    public event Action onCloseDialog;
    public event Action OnDialogFinished; 
 
    public static DialogManager Instance { get; private set; } 
 
    private void Awake(){ 
        Instance = this; 
    } 

    public bool IsShowing { get; private set;}
 
    public IEnumerator ShowDialog(Dialog dialog, List<string> choices=null, Action<int> onChoiceSelected=null) 
    { 
        yield return new WaitForEndOfFrame(); 
 
        onShowDialog?.Invoke(); 
        IsShowing = true;

        dialogBox.SetActive(true);

        foreach (var line in dialog.Lines)
        {
            AudioManager.i.PlaySfx(AudioManager.AudioId.UISelect);
            yield return TypeDialog(line);
            yield return new WaitUntil(() => keyAButton);
        }

        if (choices != null && choices.Count >1)
        {
            yield return choiceBox.ShowChoices(choices, onChoiceSelected);
        }

        dialogBox.SetActive(false);
        IsShowing = false;
        onCloseDialog?.Invoke();
    } 

    public IEnumerator ShowDialogText(string text, bool WaitForInput=true, bool autoClose=true, List<string> choices=null, Action<int> onChoiceSelected=null)
    {
        onShowDialog?.Invoke(); 
        IsShowing = true;

        dialogBox.SetActive(true);

        AudioManager.i.PlaySfx(AudioManager.AudioId.UISelect);

        yield return TypeDialog(text);

        if(WaitForInput)
        {
            yield return new WaitUntil(() => keyAButton);
        }

         if (choices != null && choices.Count >1)
        {
            yield return choiceBox.ShowChoices(choices, onChoiceSelected);
        }

        if(autoClose)
        {
            closeDialog();
        }
        OnDialogFinished?.Invoke();
    }

    public void closeDialog()
    {
        dialogBox.SetActive(false);
        IsShowing = false;
    }
 
    public void HandleUpdate() 
    { 
        
    } 
 
    public IEnumerator TypeDialog(string line) 
    { 
        dialogText.text = ""; 
        foreach (var letter in line.ToCharArray()) 
        { 
            dialogText.text += letter; 
            yield return new WaitForSeconds(1f / letterPerSecond); 
        } 
    } 

    public void MobileAWrapper()
    {
        StartCoroutine(ButtonA());
    }

    private IEnumerator ButtonA()
    {
        keyAButton = true;
        yield return new WaitForSeconds(1f / letterPerSecond);
        keyAButton = false;
    }
}