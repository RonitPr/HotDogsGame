using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player[] playerTypes;
    public CameraFollow cameraFollow;

    private int _currentIndex = 0;
    private Player _currentPlayer;

    void Start()
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

        if (_currentPlayer != null)
        {
            lastPos = _currentPlayer.transform.position;
            _currentPlayer.gameObject.SetActive(false);
        }

        _currentPlayer = playerTypes[index];
        _currentPlayer.gameObject.SetActive(true);
        _currentPlayer.transform.position = lastPos;

        cameraFollow.target = _currentPlayer.transform;
    }

    public Player GetCurrentPlayer() => _currentPlayer;
}
