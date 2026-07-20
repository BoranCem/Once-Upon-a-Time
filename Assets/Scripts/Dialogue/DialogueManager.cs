using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;

    private string[] dialogue =
    {
        "Bugün hava biraz soğuk.",
        "Yoğun bir gün geçirdim.",
        "Bana bir kokteyl hazırlar mısın?"
    };

    private int index = 0;

    void Start()
    {
        dialogueText.text = dialogue[index];
    }

    public void NextDialogue()
    {
        index++;

        if (index < dialogue.Length)
        {
            dialogueText.text = dialogue[index];
        }
    }
}