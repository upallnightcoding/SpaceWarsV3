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
        Vector2 look = Vector2.zero;

        if (context.performed)
        {
            look = Mouse.current.position.ReadValue();
        } 

        //EventManager.Instance.InvokeOnInputLook(look);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EventManager.Instance.InvokeOnFire();
        }
    }

    public void OnQuitEngagement(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            EventManager.Instance.InvokeOnQuitEngagement();
        }
    }

    public void OnFire1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            EventManager.Instance.InvokeOnFireKey(1);
        }

        if (context.canceled)
        {
            EventManager.Instance.InvokeOnFireKey(-1);
        }
    }

    public void OnFire2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            EventManager.Instance.InvokeOnFireKey(2);
        }

        if (context.canceled)
        {
            EventManager.Instance.InvokeOnFireKey(-2);
        }
    }

    public void OnFire3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            EventManager.Instance.InvokeOnFireKey(3);
        }

        if (context.canceled)
        {
            EventManager.Instance.InvokeOnFireKey(-3);
        }
    }
}
