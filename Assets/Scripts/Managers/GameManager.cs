using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public DialogueData[] days;

    private int currentDay = 0;

    private void Start()
    {
        StartDay();
    }

    public void StartDay()
    {
        if (currentDay >= days.Length)
        {
            Debug.Log("Game Finished!");
            return;
        }

        Debug.Log("Day " + (currentDay + 1) + " Started!");

        dialogueManager.StartDialogue(days[currentDay]);
    }

    public void EndDay()
    {
        Debug.Log("Day " + (currentDay + 1) + " Finished!");

        currentDay++;

        StartDay();
    }
}