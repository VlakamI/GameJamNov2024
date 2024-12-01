using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    Vector2 moveDirection = Vector2.zero;

    private float speedMultiplier = 15;
    private float jumpMultiplier = 50;
    private float horizontal;
    private List<string> fullColourList;
    private List<string> colourList;


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
        fullColourList = new List<string> { "Neutral", "Red", "Green", "Blue" };
        colourList = new List<string>();
        colourList.Add("Neutral");
        colourList.Add("Red");
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontal * speedMultiplier, rb.linearVelocity.y);
        outOfBounds(-10);
        //moveDirection = playerControls.ReadValue<Vector2>();

    }
    private void FixedUpdate()
    {
        //rb.linearVelocity = new Vector2(moveDirection.x * speedMultiplier, moveDirection.y * speedMultiplier);
    }
    private void outOfBounds(int ylevel)
    {
        if (this.transform.position.y < ylevel)
        { 
        this.transform.position = new Vector2(-2, -1);
        }
    }
    private bool IsGrounded()
    {
        //Debug.Log("check if grounded");
        return Physics2D.OverlapCircle(groundCheck.position, 0.8f, groundLayer);
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
    private void colourToggle(string colour)
    {
        for (int i = 1; i < colourList.Count; i++)
        {
            if (colour == colourList[i])
            {
                //Debug.Log("Found " + fullColourList[binding + 1]);
                Transform parentRemove = GameObject.Find(colourList[i]).transform;
                foreach (Transform j in parentRemove)
                {
                    j.GetComponent<Collider2D>().enabled = false;
                    j.GetComponent<Renderer>().enabled = false;
                    j.GetComponent<ShadowCaster2D>().enabled = false;
                }

                colourList.RemoveAt(i);
                return;
            }
        }
        //Debug.Log("adding " + fullColourList[binding + 1]);
        colourList.Add(colour);
        Transform parentAdd = GameObject.Find(colour).transform;
        foreach (Transform j in parentAdd)
        {
            j.GetComponent<Collider2D>().enabled = true;
            j.GetComponent<Renderer>().enabled = true;
            j.GetComponent<ShadowCaster2D>().enabled = true;
        }
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

                    colourToggle(fullColourList[binding + 1]);
                    break;
                }
            case 1:
                {
                    //Debug.Log("input 1");
                    colourToggle(fullColourList[binding + 1]);
                    break;
                }
            case 2:
                {
                    colourToggle(fullColourList[binding + 1]);
                    break;
                }
        }
    }
}
