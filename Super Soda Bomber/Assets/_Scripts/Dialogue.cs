using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private Text charName;
    [SerializeField] private Image charImage;
    [SerializeField] private Text message;
    [SerializeField] private Dialogue_ScriptObject[] dialogues;
    [SerializeField] private float textSpeed;

    private int dialogueIndex;
    private Dialogue_ScriptObject currentDialogue;


    void Start()
    {
        //clears the placeholder info
        message.text = "";
        dialogueIndex = 0;

        StartDialogue();
    }

    void StartDialogue(){
        currentDialogue = dialogues[dialogueIndex];

        //sets the name, image and the message
        charName.text = currentDialogue.characterName;
        charImage.sprite = currentDialogue.characterImage;
        StartCoroutine(TypeLine(dialogues[dialogueIndex]));
    }
    IEnumerator TypeLine(Dialogue_ScriptObject dialogueSO){
        foreach (char c in dialogueSO.dialogue.ToCharArray()){
            message.text += c;
            yield return new WaitForSeconds(1f/textSpeed);
        }
    }
}
