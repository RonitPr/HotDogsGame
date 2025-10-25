using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action OnAllTrapsDefeated;
    public static event Action OnGameEnd;
    
    public Player[] playerTypes;
    public CameraFollow cameraFollow;
    public Player CurrentPlayer;

    [SerializeField] private float _delayAfterDeath = 2f;
    
    private static bool _areAllTrapsDefeated;
    private int _currentIndex = 0;

    void Awake()
    {
        int randomPlayerInt = UnityEngine.Random.Range(0, playerTypes.Length);      //Added UnityEngine 
        SetActivePlayer(randomPlayerInt);
        _currentIndex = randomPlayerInt;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            CyclePlayer();
        }
    }

    void CyclePlayer()
    {
        _currentIndex = (_currentIndex + 1) % playerTypes.Length;
        SetActivePlayer(_currentIndex);
    }

    void SetActivePlayer(int index)
    {
        Vector3 lastPos = Vector3.zero;

        if (CurrentPlayer != null)
        {
            lastPos = CurrentPlayer.transform.position;
            CurrentPlayer.gameObject.SetActive(false);
        }

        CurrentPlayer = playerTypes[index];
        CurrentPlayer.gameObject.SetActive(true);
        CurrentPlayer.transform.position = lastPos;

        cameraFollow.target = CurrentPlayer.transform;
    }

    public Player GetCurrentPlayer() => CurrentPlayer;

    private void OnEnable()
    {
        Player.OnPlayerDeath += HandlePlayerDeath;
        WinItem.OnWinItemCollected += GoToWinScene;
    }

    private void OnDisable()
    {
        Player.OnPlayerDeath -= HandlePlayerDeath;
        WinItem.OnWinItemCollected -= GoToWinScene;
    }

    private void HandlePlayerDeath()
    {
        StartCoroutine(GoToLooseScene());
    }

    private IEnumerator GoToLooseScene()
    {
        yield return new WaitForSeconds(_delayAfterDeath);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Loose");
        OnGameEnd?.Invoke();
    }

    public void GoToWinScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Win");
        OnGameEnd?.Invoke();
    }

    public static void HandleAllTrapsDefeated()
    {
        if (TrapController.DefeatedCounter >= TrapSpawner.SpawnCounter)
        {
            _areAllTrapsDefeated = true;
            OnAllTrapsDefeated?.Invoke();
        }
    }

    [ContextMenu("Trigger All Traps Defeated (Debug)")]
    private void DebugTriggerAllTrapsDefeated()
    {
        _areAllTrapsDefeated = true;
        Debug.Log("Debug: All traps defeated triggered manually!");
        OnAllTrapsDefeated?.Invoke();
    }
}
