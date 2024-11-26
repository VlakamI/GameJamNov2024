using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    Vector2 moveDirection = Vector2.zero;

    private float speedMultiplier = 5;
    private float jumpMultiplier = 50;
    private float horizontal;

    //private void OnEnable()
    //{
    //    playerControls.Enable();
    //}
    //private void OnDisable()
    //{
    //    playerControls.Disable();
    //}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontal * speedMultiplier, rb.linearVelocity.y);
        //moveDirection = playerControls.ReadValue<Vector2>();

    }
    private void FixedUpdate()
    {
        //rb.linearVelocity = new Vector2(moveDirection.x * speedMultiplier, moveDirection.y * speedMultiplier);
    }
    private bool IsGrounded()
    {
        Debug.Log("check if grounded");
        return Physics2D.OverlapCircle(groundCheck.position, 0.6f, groundLayer);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("attempt to jump");
        if (context.performed && IsGrounded())
        {
            Debug.Log("jumped");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpMultiplier);
        }
        if(context.canceled && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
