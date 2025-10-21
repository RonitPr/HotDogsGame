using UnityEngine;
using UnityEngine.SceneManagement;

public class WinItem : PickupBase
{
    [SerializeField] private GameObject winCanvas;
    protected override void HandlePickupEffect()
    {
        //Debug.Log("WinItem picked up — showing Win Canvas!");
        //winCanvas.SetActive(true);
        //Time.timeScale = 0f;
        TriggerWin();
    }

    private void TriggerWin()
    {
        //if (winCanvas != null)
        //{
        //    winCanvas.SetActive(true);
        //}
        //Time.timeScale = 0f;
        GoToWinScene();
    }

    public void GoToWinScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Win");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Intro");
    }
}

