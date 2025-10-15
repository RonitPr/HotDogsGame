using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player[] playerTypes;
    public CameraFollow cameraFollow;

    private int _currentIndex = 0;
    public Player CurrentPlayer;

    void Awake()
    {
        int randomPlayerInt = Random.Range(0, playerTypes.Length);
        SetActivePlayer(randomPlayerInt);
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
}
