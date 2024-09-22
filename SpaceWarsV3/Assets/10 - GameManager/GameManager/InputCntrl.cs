using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputCntrl : MonoBehaviour
{
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 move = context.ReadValue<Vector2>();
            EventManager.Instance.InvokeOnInputMove(move);
        }

        if (context.canceled)
        {
            Vector3 move = Vector2.zero;
            EventManager.Instance.InvokeOnInputMove(move);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 look = Mouse.current.position.ReadValue();
            EventManager.Instance.InvokeOnInputLook(look);
        }
    }
}
