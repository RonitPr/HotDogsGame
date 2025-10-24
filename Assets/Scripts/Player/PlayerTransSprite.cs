using UnityEngine;

public class PlayerTransSprite : MonoBehaviour
{
    [SerializeField] Player player;

    public SpriteRenderer SR; 
    private bool _facingLeft = true; // the assets sprites are facing left
    
    private void Start()
    {
        SR =  GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        SR.sprite = player.GetComponent<SpriteRenderer>().sprite;

        Vector3 inputDirection = player.GetInputDirection();
        if (inputDirection.x < 0 && !_facingLeft || inputDirection.x > 0 && _facingLeft)
        {
            Flip();
        }
    }

    void Flip()
    {
        GetComponent<SpriteRenderer>().flipX = _facingLeft;
        _facingLeft = !_facingLeft;
    }
}
