using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    Vector2 moveDirection = Vector2.zero;

    private float speedMultiplier = 15;
    private float jumpMultiplier = 50;
    private float horizontal;
    private string[] fullColourList = { "Neutral", "Red", "Green", "Blue" };
    private string[] colourList;
    
    
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
        colourList = new string[fullColourList.Length];
        colourList[0] = "Neutral";
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
        //Debug.Log("check if grounded");
        return Physics2D.OverlapCircle(groundCheck.position, 0.7f, groundLayer);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("attempt to jump");
        if (context.performed && IsGrounded())
        {
            if (ColourCheck())
            {
                //Debug.Log("jumped");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpMultiplier);
            }
        }
        if (context.canceled && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
    private bool ColourCheck()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(groundCheck.position, 0.7f, groundLayer);
        foreach (Collider2D e in col)
        {
            if (ColourMatch(e.gameObject.transform.parent.name))
            {
                return true;
            }
        }
        return false;
    }
    private bool ColourMatch(string inp)
    {
        foreach (string i in colourList)
        {
            if (i == inp)
            {
                return true;
            }
        }
        return false;
    }
    public void Colour(InputAction.CallbackContext context)
    {
        //var binding = context.action.GetBindingForControl(context.control);
        int binding = context.action.GetBindingIndexForControl(context.control);

        switch (binding)
        {

            case 0:
                {
                    //Debug.Log("input 0");

                    for(int i=0; i < colourList.Length; i++)
                    {
                        if(fullColourList[binding + 1] == colourList[i])
                        {
                            colourList[i] = "";
                            break;
                        }
                    }
                    
                    break;
                }
            case 1:
                {
                    //Debug.Log("input 1");

                    break;
                }
            case 2:
                {
                    
                    break;
                }
        }
    }
}
