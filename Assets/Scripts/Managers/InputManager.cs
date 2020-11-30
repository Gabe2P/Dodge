﻿//Written by Gabriel Tupy 11-28-2020
//Last modified by Gabriel Tupy 11-28-2020

using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void StateChange(inputDirection newDirection);
    public event StateChange onDirectionChange;

    public enum inputDirection { None = 0, Left = -1, Right = 1};
    [SerializeField] private inputDirection curDirection;

    public bool useKeyBoardInput = false;

    private void Update()
    {
        if (useKeyBoardInput)
        {
            SetInputDirection(Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")));
        }
    }

    private void SetInputDirection(int directionInt)
    {
        switch (directionInt)
        {
            case 0:
                curDirection = inputDirection.None;
                break;
            case -1:
                curDirection = inputDirection.Left;
                break;
            case 1:
                curDirection = inputDirection.Right;
                break;
        }
        onDirectionChange?.Invoke(curDirection);
    }

    public void SetInputToKeyboard(bool condition)
    {
        useKeyBoardInput = condition;
    }

    public void SetInputDirectionToLeft()
    {
        curDirection = inputDirection.Left;
        onDirectionChange?.Invoke(curDirection);
    }
    public void SetInputDirectionToRight()
    {
        curDirection = inputDirection.Right;
        onDirectionChange?.Invoke(curDirection);
    }
    public void SetInputDirectionToNone()
    {
        curDirection = inputDirection.None;
        onDirectionChange?.Invoke(curDirection);
    }
}
