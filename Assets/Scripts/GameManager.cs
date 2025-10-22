using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static event Action OnAllTrapsDefeated;
    
    public static GameManager Instance;
    public Player[] playerTypes;
    public CameraFollow cameraFollow;
    public Player CurrentPlayer;
    public int TrapsDefeated => _trapsDefeated;
    public int TotalTrapsSpawned => _totalTrapsSpawned;
    public static bool AreAllTrapsDefeated => _areAllTrapsDefeated;


    [SerializeField] private float _delayAfterDeath = 2f;
    
    private static bool _areAllTrapsDefeated;
    private int _currentIndex = 0;
    private int _trapsDefeated = 0;
    private int _totalTrapsSpawned = 0;


    void Awake()
    {
        int randomPlayerInt = UnityEngine.Random.Range(0, playerTypes.Length);      //Added UnityEngine 
        SetActivePlayer(randomPlayerInt);

        // SAFE SINGLETON PATTERN
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Destroy duplicate managers
            return;
        }

        Instance = this;
        UIManager.Instance?.UpdateTrapCounter(_trapsDefeated, _totalTrapsSpawned);
        //DontDestroyOnLoad(gameObject); // Keep it alive between scene loads
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

    public void RegisterTrap(IGenericTrap trap)
    {
        _totalTrapsSpawned++;
        trap.OnDefeated += () => AddTrapDefeat();
    }

    private void AddTrapDefeat()
    {
        _trapsDefeated++;
        Debug.Log($"Traps Defeated: {_trapsDefeated}");
        UIManager.Instance?.UpdateTrapCounter(_trapsDefeated, _totalTrapsSpawned);

        HandleAllTrapsDefeated();
    }

    private void OnEnable()
    {
        Player.OnPlayerDeath += HandlePlayerDeath;
        WinItem.OnWinItemCollected += GoToWinScene;
    }

    //private void OnDisable()      // I'm not sure if needed
    //{
    //    Player.OnPlayerDeath -= HandlePlayerDeath;
    //}

    private void HandlePlayerDeath()
    {
        StartCoroutine(GoToLooseScene());
    }

    private IEnumerator GoToLooseScene()
    {
        yield return new WaitForSeconds(_delayAfterDeath);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Loose");
    }

    public void GoToWinScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Win");
    }

    private void HandleAllTrapsDefeated()
    {
        if (_trapsDefeated >= _totalTrapsSpawned)
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
