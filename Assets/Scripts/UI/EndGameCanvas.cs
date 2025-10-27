using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameCanvas : MonoBehaviour
{
    public string introScene;
    public GameObject backButton;
    public GameObject text;
    public int timeToWaitforButton = 10;

    public void GoToIntroScene()
    {
        SceneManager.LoadScene(introScene);
    }
    void Start()
    {
        StartCoroutine(ShowCanvasElements());
    }
    IEnumerator ShowCanvasElements()
    {
        backButton.SetActive(false);
        text.SetActive(false);
        yield return new WaitForSeconds(timeToWaitforButton);
        backButton.SetActive(true);
        text.SetActive(true);
    }
}
