using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [TextArea] public string introText;

    public DialogueChoice choice1;
    public DialogueChoice choice2;
    public DialogueChoice choice3;
    public DialogueChoice choice4;

    private DialogueUI dialogueUI;

    private void Awake()
    {
        dialogueUI = FindObjectOfType<DialogueUI>();
    }

    public void Interact()
    {
        dialogueUI?.ShowDialogue(introText, choice1, choice2, choice3, choice4);
    }
}
