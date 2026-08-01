using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text characterName;
    public TMP_Text dialogueText;

    [Header("Dialogue")]
    public DialogueData dialogue;

    [Header("Panels")]
    public GameObject cocktailPanel;

    private int index = 0;

    void Start()
{
    cocktailPanel.SetActive(false);

    if (dialogue != null)
    {
        StartDialogue(dialogue);
    }
}

    public void StartDialogue(DialogueData newDialogue)
    {
        dialogue = newDialogue;
        index = 0;

        ShowDialogue();
    }

    private void ShowDialogue()
    {
        characterName.text = dialogue.lines[index].speaker.characterName;
        characterName.color = dialogue.lines[index].speaker.nameColor;

        dialogueText.text = dialogue.lines[index].text;
    }

    public void NextDialogue()
{
    index++;

    if (index >= dialogue.lines.Length)
    {
        Debug.Log("Dialogue Finished!");
        return;
    }

    DialogueLine currentLine = dialogue.lines[index];

    switch (currentLine.action)
    {
        case DialogueAction.WaitForCocktail:

            dialogueText.text = "";
            characterName.text = "";

            OpenCocktailPanel();

            break;

        default:

            ShowDialogue();

            break;
    }
}
public void OpenCocktailPanel()
{
    cocktailPanel.SetActive(true);

    RectTransform panel = cocktailPanel.GetComponent<RectTransform>();
    panel.anchoredPosition = Vector2.zero;
}

public void ContinueDialogue()
{
    ShowDialogue();
}

}