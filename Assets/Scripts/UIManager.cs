using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI trapCounterText;
    [SerializeField] private TextMeshProUGUI hoverText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        CameraFollow.OnHoverStarted += ShowHoverText;
        CameraFollow.OnHoverAfterTrapsDefeated += ShowTrapsDefeatedHoverText;
        CameraFollow.OnHoverEnded += HideHoverText;
    }

    private void ShowTrapsDefeatedHoverText()
    {
        hoverText.text = "The path is clear!";
        hoverText.gameObject.SetActive(true);
    }

    private void ShowHoverText()
    {
        hoverText.text = "Your goal lies ahead...";
        hoverText.gameObject.SetActive(true);
    }

    private void HideHoverText()
    {
        hoverText.gameObject.SetActive(false);
    }

    public void UpdateTrapCounter(int defeated, int total)
    {
        if (trapCounterText != null)
            trapCounterText.text = $"Traps Defeated: {defeated}/{total}";
    }
}
