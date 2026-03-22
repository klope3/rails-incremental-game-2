using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputActionsProvider
{
    private static InputSystem_Actions inputActions;

    public static event System.Action OnClickStarted;
    public static event System.Action OnClickCanceled;

    public static void Initialize()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();

        inputActions.Player.Attack.started += Click_started;
        inputActions.Player.Attack.canceled += Click_canceled;
    }

    public static void Disable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Attack.started -= Click_started;
        inputActions.Player.Attack.canceled -= Click_canceled;
    }

    private static void Click_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnClickStarted?.Invoke();
    }

    private static void Click_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnClickCanceled?.Invoke();
    }

    public static Vector2 GetMousePosition()
    {
        return UnityEngine.InputSystem.Mouse.current.position.ReadValue();
    }
}
