using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameCanvas : MonoBehaviour
{
    public string introScene;
    public GameObject backButton;
    public int timeToWaitforButton = 10;

    public void GoToIntroScene()
    {
        SceneManager.LoadScene(introScene);
    }
    void Start()
    {
        StartCoroutine(ShowButton());
    }
    IEnumerator ShowButton()
    {
        backButton.SetActive(false);
        yield return new WaitForSeconds(timeToWaitforButton);
        backButton.SetActive(true);
    }
}
