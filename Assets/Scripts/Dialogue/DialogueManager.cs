using System.Collections;
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

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip customerArrivalSound;

    [Header("Typewriter Effect")]
    public float typingSpeed = 30f;

    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private string currentFullText;
    private int index = 0;

    public GameManager gameManager;

    public GameObject dialoguePanel;
    public DialogueLine CurrentLine
    {
        get
        {
            return dialogue.lines[index];
        }
    }

    private void Start()
{
    cocktailPanel.SetActive(false);
    StartCoroutine(StartDialogueAfterDelay());
}

private IEnumerator StartDialogueAfterDelay()
{
    if (dialoguePanel != null)
        dialoguePanel.SetActive(false);

    yield return new WaitForSeconds(5f);

    if (dialoguePanel != null)
        dialoguePanel.SetActive(true);

    if (gameManager != null)
        gameManager.StartDay();
}

    public void StartDialogue(DialogueData newDialogue)
    {
        dialogue = newDialogue;
        index = 0;

        // Play the customer arrival sound in Story Mode.
        if (audioSource != null && customerArrivalSound != null)
        {
            audioSource.PlayOneShot(customerArrivalSound);
        }

        ShowDialogue();
    }

    private void ShowDialogue()
{
    DialogueLine line = dialogue.lines[index];

    // Müşteri geliş sesi
    if (line.action == DialogueAction.CustomerArrival)
    {
        if (audioSource != null && customerArrivalSound != null)
        {
            audioSource.PlayOneShot(customerArrivalSound);
        }
    }

    characterName.text = line.speaker.characterName;
    characterName.color = line.speaker.nameColor;

    currentFullText = line.text;
    dialogueText.text = "";

    if (typingCoroutine != null)
    {
        StopCoroutine(typingCoroutine);
    }

    typingCoroutine = StartCoroutine(TypeText());
}

    private IEnumerator TypeText()
{
    isTyping = true;
    dialogueText.text = "";

    foreach (char letter in currentFullText)
    {
        dialogueText.text += letter;
        yield return new WaitForSeconds(1f / typingSpeed);
    }

    dialogueText.text = currentFullText;
    isTyping = false;
    typingCoroutine = null;
}

public void NextDialogue()
{
    if (isTyping)
{
    if (typingCoroutine != null)
    {
        StopCoroutine(typingCoroutine);
        typingCoroutine = null;
    }

    dialogueText.text = currentFullText;
    isTyping = false;
    return;
}
    
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

        case DialogueAction.EndDay:
            gameManager.EndDay();
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
        index++;

        if (index >= dialogue.lines.Length)
        {
            Debug.Log("Dialogue Finished!");
            return;
        }

        ShowDialogue();
    }

    public void CloseCocktailPanel()
    {
        cocktailPanel.SetActive(false);
    }
}
