using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player[] playerTypes;
    public CameraFollow cameraFollow;

    private int _currentIndex = 0;
    public Player CurrentPlayer;

    private int trapsDefeated = 0;
    private int totalTrapsSpawned = 0;
    public int TrapsDefeated => trapsDefeated;
    public int TotalTrapsSpawned => totalTrapsSpawned;

    void Awake()
    {
        int randomPlayerInt = Random.Range(0, playerTypes.Length);
        SetActivePlayer(randomPlayerInt);

        // --- SAFE SINGLETON PATTERN ---
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Destroy duplicate managers
            return;
        }

        Instance = this;
        UIManager.Instance?.UpdateTrapCounter(trapsDefeated, totalTrapsSpawned);
        DontDestroyOnLoad(gameObject); // Keep it alive between scene loads
        //if (Instance == null)
        //{
        //    Instance = this;
        //}
        //else
        //    Destroy(gameObject);
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
        totalTrapsSpawned++;
        trap.OnDefeated += () => AddTrapDefeat();
    }

    private void AddTrapDefeat()
    {
        trapsDefeated++;
        Debug.Log($"Traps Defeated: {trapsDefeated}");
        UIManager.Instance?.UpdateTrapCounter(trapsDefeated, totalTrapsSpawned);
    }
}
