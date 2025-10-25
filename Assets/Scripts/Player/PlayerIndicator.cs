using UnityEngine;
using UnityEngine.UI;

public class PlayerIndicator : MonoBehaviour
{
    public Sprite BunnyChosen, DogChosen, CatChosen;
    public GameManager Manager;
    private Image IndicatorImage;

    private void OnEnable()
    {
        GameManager.OnPlayerSwitch += SetPlayerChosenSprite;
    }

    private void OnDisable()
    {
        GameManager.OnPlayerSwitch -= SetPlayerChosenSprite;
    }

    private void Start()
    {
        IndicatorImage = GetComponent<Image>();
        SetPlayerChosenSprite();
    }

    public void SetPlayerChosenSprite()
    {
        if (Manager == null) return;
        switch (Manager.CurrentPlayer.GetType().Name)
        {
            case "DogPlayer":
                IndicatorImage.sprite = DogChosen;
                break;
            case "CatPlayer":
                IndicatorImage.sprite = CatChosen;
                break;
            case "BunnyPlayer":
                IndicatorImage.sprite = BunnyChosen;
                break;

        }
    }
}
