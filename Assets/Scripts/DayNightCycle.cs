using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image nightOverlay;  // UI Image covering the screen

    [Header("Timing Settings")]
    [SerializeField] private float transitionDuration = 180f;
    [SerializeField] private bool useUnscaledTime = false;     // if true, ignores Time.timeScale

    [Header("Cycle Settings")]
    [SerializeField] private bool loopCycle = true;            // keep looping day ↔ night
    [SerializeField] private bool startAsNight = true;         // start dark or bright

    [Header("Colors")]
    [SerializeField] private Color nightColor = new Color(0.1f, 0.05f, 0.2f, 0.6f);
    [SerializeField] private Color dayColor = new Color(0.1f, 0.05f, 0.2f, 0f); // transparent (daylight)

    [Header("Game Over Option")]
    [SerializeField] private bool triggerGameOverOnDay = false;  // end game when reaching day

    private Coroutine cycleCoroutine;
    private bool isNight;

    private void Start()
    {
        if (nightOverlay == null)
        {
            Debug.LogWarning("DayNightCycle: No nightOverlay assigned!");
            return;
        }
        nightOverlay.gameObject.SetActive(true);


        // initialize start color
        isNight = startAsNight;
        nightOverlay.color = isNight ? nightColor : dayColor;

        cycleCoroutine = StartCoroutine(CycleCoroutineNightDay());
    }

    private IEnumerator CycleCoroutineNightDay()
    {
        // loop indefinitely (or once, if loopCycle = false)
        do
        {
            // decide which direction to fade
            if (isNight)
                yield return StartCoroutine(FadeRoutine(nightColor, dayColor)); // fade to day
            else
                yield return StartCoroutine(FadeRoutine(dayColor, nightColor)); // fade to night

            // toggle state
            isNight = !isNight;

            // trigger "game over" if it reached day
            if (!isNight && triggerGameOverOnDay)
            {
                Debug.Log("Day reached — triggering Game Over!");
                OnGameOver();
                yield break; // stop the cycle
            }

        } while (loopCycle);
    }

    private IEnumerator FadeRoutine(Color startColor, Color endColor)
    {
        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / transitionDuration);
            // Optional easing:
            // t = Mathf.SmoothStep(0f, 1f, t);

            nightOverlay.color = Color.Lerp(startColor, endColor, t);
            yield return null; // wait one frame (then change)
        }

        nightOverlay.color = endColor;
    }

    private void OnGameOver()
    {
        // You can customize this: call GameManager.GameOver(), show canvas, etc.
        // Example:
        // GameManager.Instance.ShowGameOverScreen();

        Debug.Log("Game Over triggered by day cycle.");
    }
}


