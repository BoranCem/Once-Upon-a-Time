using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text characterName;
    public TMP_Text dialogueText;

    [Header("Dialogue")]
    public DialogueData dialogue;

    private int index = 0;

    void Start()
    {
        // Eğer Inspector'dan bir diyalog atanmışsa otomatik başlat
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
        characterName.text = dialogue.lines[index].speaker;
        dialogueText.text = dialogue.lines[index].text;
    }

    public void NextDialogue()
    {
        index++;

        if (index < dialogue.lines.Length)
        {
            ShowDialogue();
        }
        else
        {
            Debug.Log("Dialogue Finished");

            // Buraya daha sonra:
            // CustomerManager.NextCustomer();
            // veya
            // CocktailPanel.SetActive(true);
            // yazacağız.
        }
    }
}