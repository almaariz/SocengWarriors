using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class ChoiceBox : MonoBehaviour
{
    [SerializeField] ChoiceText choiceTextPrefab;
    bool choiceSelected = false;
    List<ChoiceText> choiceTexts;
    int currentChoices;
    bool keyAButton;
    public IEnumerator ShowChoices(List<string> choices, Action<int> onChoiceSelected)
    {
        choiceSelected = false;
        currentChoices = 0;

        gameObject.SetActive(true);

        foreach (Transform child in transform)
            Destroy(child.gameObject);
        
        choiceTexts = new List<ChoiceText>();
        foreach (var choice in choices)
        {
            var choiceTextObj = Instantiate(choiceTextPrefab, transform);
            choiceTextObj.TextField.text = choice;
            choiceTexts.Add(choiceTextObj);
        }

        yield return new WaitUntil(() => choiceSelected == true);

        onChoiceSelected?.Invoke(currentChoices);
        gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            ++currentChoices;
        else if (Input.GetKeyDown(KeyCode.X))
            --currentChoices;

        currentChoices = Mathf.Clamp(currentChoices, 0, choiceTexts.Count - 1);

        for (int i = 0; i < choiceTexts.Count; i++)
        {

        }

        if (keyAButton)
        {
            choiceSelected = true;
        }
    }

    public void MobileAWrapper()
    {
        StartCoroutine(ButtonA());
    }

    private IEnumerator ButtonA()
    {
        keyAButton = true;
        yield return new WaitForSeconds(1f);
        keyAButton = false;
    }
}
