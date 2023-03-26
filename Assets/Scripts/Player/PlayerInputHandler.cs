using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }

    [SerializeField]
    private float jumpBufferTime = 0.2f;

    private float jumpStartTime;

    private void Update()
    {
        CheckJumpBuffer();
    }

    private void CheckJumpBuffer()
    {
        if (Time.time >= jumpStartTime + jumpBufferTime)
        {
            JumpInput = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            jumpStartTime = Time.time;
        }
    }

    public void UseJupmInput() => JumpInput = false;

    public void OnShootInput(InputAction.CallbackContext context)
    {
       // Debug.Log("Shoot");
    }
    public void OnAbilityActivateInput(InputAction.CallbackContext context)
    {
        Debug.Log("Ability");
    }
}
