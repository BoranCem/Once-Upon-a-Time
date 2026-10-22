
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [Header("Customer")]
    public List<CharacterData> customers = new List<CharacterData>();
    public TMP_Text customerNameText;
    public Image customerPortrait;

    [Header("Cocktail UI")]
    public GameObject cocktailPanel;

    [Header("Current Order")]
    public PropertyLevel requiredSweetness;
    public PropertyLevel requiredSourness;
    public PropertyLevel requiredBitterness;
    public PropertyLevel requiredStrength;
    public PropertyLevel requiredFreshness;

    private CharacterData currentCustomer;
    private CharacterData previousCustomer;

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

        ChooseRandomCustomer();

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

    private void ChooseRandomCustomer()
    {
        if (customers == null || customers.Count == 0)
        {
            currentCustomer = null;
            Debug.LogWarning(
                "Rush Mode: Add CharacterData assets to the Customers list."
            );

            if (customerNameText != null)
                customerNameText.text = "Customer";

            if (customerPortrait != null)
                customerPortrait.enabled = false;

            return;
        }

        List<CharacterData> availableCustomers =
            new List<CharacterData>();

        foreach (CharacterData customer in customers)
        {
            if (customer != null)
                availableCustomers.Add(customer);
        }

        if (availableCustomers.Count == 0)
        {
            currentCustomer = null;
            Debug.LogWarning(
                "Rush Mode: The Customers list contains no valid characters."
            );
            return;
        }

        if (previousCustomer != null &&
            availableCustomers.Count > 1)
        {
            availableCustomers.Remove(previousCustomer);
        }

        int randomIndex =
            Random.Range(0, availableCustomers.Count);

        currentCustomer = availableCustomers[randomIndex];
        previousCustomer = currentCustomer;

        if (customerNameText != null)
        {
            customerNameText.text = currentCustomer.characterName;
            customerNameText.color = currentCustomer.nameColor;
        }

        if (customerPortrait != null)
        {
            customerPortrait.sprite = currentCustomer.portrait;
            customerPortrait.enabled =
                currentCustomer.portrait != null;
        }
    }

    private void UpdateOrderText()
    {
        if (orderText == null)
            return;

        List<string> requests = new List<string>();

        AddRequest(
            requests, requiredSweetness,
            "not too sweet",
            "nicely balanced sweetness",
            "quite sweet"
        );

        AddRequest(
            requests, requiredSourness,
            "not too sour",
            "a little tangy",
            "quite sour"
        );

        AddRequest(
            requests, requiredBitterness,
            "not too bitter",
            "a hint of bitterness",
            "quite bitter"
        );

        AddRequest(
            requests, requiredStrength,
            "light",
            "moderately strong",
            "strong"
        );

        AddRequest(
            requests, requiredFreshness,
            "a mellow taste",
            "a fresh and balanced taste",
            "very refreshing"
        );

        string customerName = currentCustomer != null
            ? currentCustomer.characterName
            : "Customer";

        orderText.text =
            customerName + ":\n" + BuildRequestSentence(requests);
    }

    private void AddRequest(
        List<string> requests,
        PropertyLevel level,
        string lowDescription,
        string mediumDescription,
        string highDescription)
    {
        switch (level)
        {
            case PropertyLevel.Low:
                requests.Add(lowDescription);
                break;

            case PropertyLevel.Medium:
                requests.Add(mediumDescription);
                break;

            case PropertyLevel.High:
                requests.Add(highDescription);
                break;
        }
    }

    private string BuildRequestSentence(List<string> requests)
    {
        if (requests.Count == 0)
            return "Surprise me! Make me something nice.";

        if (requests.Count == 1)
            return GetSingleRequest(requests[0]);

        if (requests.Count == 2)
        {
            return "Could I get something that's "
                + requests[0] + " and "
                + requests[1] + "?";
        }

        string combined = "";

        for (int i = 0; i < requests.Count; i++)
        {
            if (i > 0)
            {
                combined += i == requests.Count - 1
                    ? ", and "
                    : ", ";
            }

            combined += requests[i];
        }

        return "I'd like a drink that's "
            + combined + ", please.";
    }

    private string GetSingleRequest(string request)
    {
        switch (request)
        {
            case "not too sweet":
                return "Could I get something that's not too sweet, please?";

            case "nicely balanced sweetness":
                return "I'd like something with balanced sweetness, please.";

            case "quite sweet":
                return "Could you make me something sweet?";

            case "not too sour":
                return "I'd prefer something that's not too sour.";

            case "a little tangy":
                return "Could I get something a little tangy?";

            case "quite sour":
                return "I'd love something with a strong sour taste.";

            case "not too bitter":
                return "Could I get something that's not too bitter?";

            case "a hint of bitterness":
                return "I'd like a drink with just a hint of bitterness.";

            case "quite bitter":
                return "Could you make me something quite bitter?";

            case "light":
                return "I'd like something light, please.";

            case "moderately strong":
                return "Could I get something moderately strong?";

            case "strong":
                return "I could really use a strong drink.";

            case "a mellow taste":
                return "Could you make me something with a mellow taste?";

            case "a fresh and balanced taste":
                return "I'd like something fresh and balanced.";

            case "very refreshing":
                return "Could I get something really refreshing?";

            default:
                return "Could I get something like that, please?";
        }
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
