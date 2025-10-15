using UnityEngine;
using UnityEngine.UI;
public class HeartController : MonoBehaviour
{
    public Sprite FullHeart, HalfHeart, EmptyHeart;
    Image HeartImage;

    private void Awake()
    {
        HeartImage = GetComponent<Image>();
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                HeartImage.sprite = EmptyHeart;
                break;
            case HeartStatus.Half:
                HeartImage.sprite = HalfHeart;
                break;
            case HeartStatus.Full:
                HeartImage.sprite = FullHeart;
                break;
        }
    }
}

public enum HeartStatus 
{
    Empty = 0,
    Half = 1,
    Full = 2
}

