using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI trapCounterText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateTrapCounter(int defeated, int total)
    {
        if (trapCounterText != null)
            trapCounterText.text = $"Traps Defeated: {defeated}/{total}";
    }
}
