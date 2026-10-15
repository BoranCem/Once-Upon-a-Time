
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject modeSelectionPanel;

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);
        modeSelectionPanel.SetActive(true);
    }

    public void StartStoryMode()
    {
        SceneManager.LoadScene("BarScene");
    }

    public void StartRushMode()
    {
        SceneManager.LoadScene("RushMode");
    }

    public void ReturnToMainMenu()
    {
        modeSelectionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
