using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text characterName;
    public TMP_Text dialogueText;

    public DialogueData dialogue;
    private int index = 0;

    void Start()
    {
        ShowDialogue();
    }

    private void ShowDialogue()
{
    characterName.text = dialogue.lines[index].speaker;
    dialogueText.text = dialogue.lines[index].text;
}

    public void NextDialogue()
{
    index++;

    if(index < dialogue.lines.Length)
    {
        ShowDialogue();
    }
}
}