using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player[] playerTypes;
    private int currentIndex = 0;
    private Player currentPlayer;

    public CameraFollow cameraFollow;

    void Start()
    {
        SetActivePlayer(0);
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
        currentIndex = (currentIndex + 1) % playerTypes.Length;
        SetActivePlayer(currentIndex);
    }

    void SetActivePlayer(int index)
    {
        if (currentPlayer != null) currentPlayer.gameObject.SetActive(false);

        currentPlayer = playerTypes[index];
        currentPlayer.gameObject.SetActive(true);

        cameraFollow.target = currentPlayer.transform;
    }

    public Player GetCurrentPlayer() => currentPlayer;
}
