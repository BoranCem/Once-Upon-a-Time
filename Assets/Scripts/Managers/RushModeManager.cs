using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RushModeManager : MonoBehaviour
{
    [Header("Game Settings")]
    public float timeLimit = 180f;
    public int correctScore = 100;
    public int wrongPenalty = 50;

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public TMP_Text orderText;
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;

    [Header("Cocktail UI")]
public GameObject cocktailPanel;

    [Header("Current Order")]
    public PropertyLevel requiredSweetness;
    public PropertyLevel requiredSourness;
    public PropertyLevel requiredBitterness;
    public PropertyLevel requiredStrength;
    public PropertyLevel requiredFreshness;

    private float remainingTime;
    private int score;
    private int completedOrders;
    private bool gameRunning;

private void Start()
{
    remainingTime = timeLimit;
    score = 0;
    completedOrders = 0;
    gameRunning = true;

    if (gameOverPanel != null)
        gameOverPanel.SetActive(false);

    if (cocktailPanel != null)
        cocktailPanel.SetActive(true);

    GenerateOrder();
    UpdateUI();
}

    private void Update()
    {
        if (!gameRunning)
            return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            gameRunning = false;

            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            if (finalScoreText != null)
            {
                finalScoreText.text =
                    "Final Score: " + score +
                    "\nOrders Completed: " + completedOrders;
            }
        }

        UpdateUI();
    }

    private void GenerateOrder()
    {
        requiredSweetness = PropertyLevel.None;
        requiredSourness = PropertyLevel.None;
        requiredBitterness = PropertyLevel.None;
        requiredStrength = PropertyLevel.None;
        requiredFreshness = PropertyLevel.None;

        int requirementCount;

        if (completedOrders < 3)
            requirementCount = 1;
        else if (completedOrders < 6)
            requirementCount = 2;
        else if (completedOrders < 9)
            requirementCount = 3;
        else
            requirementCount = Random.Range(3, 6);

        List<int> attributes = new List<int>
        {
            0, 1, 2, 3, 4
        };

        for (int i = 0; i < requirementCount; i++)
        {
            int randomIndex = Random.Range(0, attributes.Count);
            int attribute = attributes[randomIndex];

            attributes.RemoveAt(randomIndex);

            PropertyLevel level =
                (PropertyLevel)Random.Range(1, 4);

            switch (attribute)
            {
                case 0:
                    requiredSweetness = level;
                    break;

                case 1:
                    requiredSourness = level;
                    break;

                case 2:
                    requiredBitterness = level;
                    break;

                case 3:
                    requiredStrength = level;
                    break;

                case 4:
                    requiredFreshness = level;
                    break;
            }
        }

        UpdateOrderText();
    }

    private void UpdateOrderText()
    {
        if (orderText == null)
            return;

        List<string> requests = new List<string>();

        AddRequest(requests, "Sweetness", requiredSweetness);
        AddRequest(requests, "Sourness", requiredSourness);
        AddRequest(requests, "Bitterness", requiredBitterness);
        AddRequest(requests, "Strength", requiredStrength);
        AddRequest(requests, "Freshness", requiredFreshness);

        orderText.text = "Customer Order:\n"
            + string.Join("\n", requests);
    }

    private void AddRequest(
        List<string> requests,
        string attribute,
        PropertyLevel level)
    {
        if (level == PropertyLevel.None)
            return;

        string description = level switch
        {
            PropertyLevel.Low => "Low",
            PropertyLevel.Medium => "Medium",
            PropertyLevel.High => "High",
            _ => ""
        };

        requests.Add(attribute + ": " + description);
    }

    public bool CheckRequirements(
        PropertyLevel sweetness,
        PropertyLevel sourness,
        PropertyLevel bitterness,
        PropertyLevel strength,
        PropertyLevel freshness)
    {
        return Matches(requiredSweetness, sweetness)
            && Matches(requiredSourness, sourness)
            && Matches(requiredBitterness, bitterness)
            && Matches(requiredStrength, strength)
            && Matches(requiredFreshness, freshness);
    }

    private bool Matches(
        PropertyLevel required,
        PropertyLevel actual)
    {
        return required == PropertyLevel.None
            || required == actual;
    }

    public void SubmitDrink(bool correct)
    {
        if (!gameRunning)
            return;

        if (correct)
        {
            score += correctScore;
            Debug.Log("Correct drink! +" + correctScore);
        }
        else
        {
            score = Mathf.Max(0, score - wrongPenalty);
            Debug.Log("Wrong drink! -" + wrongPenalty);
        }

        completedOrders++;

        GenerateOrder();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (timerText != null)
        {
            timerText.text =
                Mathf.CeilToInt(remainingTime).ToString();
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}