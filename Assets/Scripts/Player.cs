using UnityEditor.Playables;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public abstract Ability UseSpecialAbility();

    public float moveSpeed = 5f;

    public Rigidbody2D body;
    protected Vector3 inputDirection;

    void Update()
    {
        HandleMovement();
    }

    private void FixedUpdate()
    {
        body.linearVelocity = inputDirection * moveSpeed;

    }

    protected void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        inputDirection = new Vector3 (moveX, moveY, 0);
    }
}
