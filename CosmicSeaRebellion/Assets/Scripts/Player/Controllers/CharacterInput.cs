using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    public Vector2 deltaMove;
    public Vector2 deltaLook;

    InputPlayer input;
    float _horizontalInput;
    float _verticalInput;
    bool rb_Input;
    bool rt_Input;

    public void Desactivate()
    {
        _horizontalInput = 0;
        _verticalInput = 0;
        deltaLook = Vector2.zero;
        deltaMove = Vector2.zero;
    }

    private void OnEnable()
    {
        input = new InputPlayer();
        input.Enable();
    }

    public void GetInput()
    {
        deltaLook = input.PlayerMovement.Look.ReadValue<Vector2>();
        deltaMove = input.PlayerMovement.Move.ReadValue<Vector2>();
    }

    public bool TestInput()
    {
        return input.PlayerMovement.Test.triggered;
    }

    public bool IsAttackHeavyKeyPressed()
    {
        return input.PlayerMovement.AtackHeavy.triggered;
    }

    public bool IsAttackLigthKeyPressed()
    {
        return input.PlayerMovement.AtackLight.triggered;
    }

    public bool IsJumpKeyPressed()
    {
        return input.PlayerMovement.Jump.triggered;
    }

    public bool IsCrouchKeyPressed()
    {
        return input.PlayerMovement.Crouch.triggered;
    }

    public bool IsRollKeyPressed()
    {
        return input.PlayerMovement.Roll.triggered;
    }

    public float GetHorizontalMovementInput()
	{
        _horizontalInput = deltaMove.x;
		return _horizontalInput;
	}

	public  float GetVerticalMovementInput()
	{
        _verticalInput = deltaMove.y;
		return _verticalInput;
	}
}
