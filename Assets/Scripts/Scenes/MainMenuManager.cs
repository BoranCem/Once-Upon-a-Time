

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject modeSelectionPanel;

    // Ana menüde mod seçme panelini açar.
    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);
        modeSelectionPanel.SetActive(true);
    }

    // Story Mode
    public void StartStoryMode()
    {
        SceneManager.LoadScene("BarScene");
    }

    // Rush Mode
    public void StartRushMode()
{
    SceneManager.LoadScene("RushModeScene");
}

    // Ana menü içindeki mod seçiminden geri döner.
    public void ReturnToMainMenu()
    {
        if (modeSelectionPanel != null)
            modeSelectionPanel.SetActive(false);

        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }

    // BarScene veya RushMode'dan ana menü sahnesine döner.
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
