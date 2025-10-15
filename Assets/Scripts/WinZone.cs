using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private GameObject winCanvas;
    [Header("Pickup Animation")]
    [SerializeField] private Animator animator;

    private bool isPickedUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPickedUp) { return; }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Triggering pickup animation!");
            isPickedUp = true;
            animator.SetBool("isPickedUp", true);
        }
    }

    public void OnPickupAnimationEnd()
    {
        TriggerWin();
        gameObject.SetActive(false);
    }

    private void TriggerWin()
    {
        if (winCanvas != null)
        {
            winCanvas.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
