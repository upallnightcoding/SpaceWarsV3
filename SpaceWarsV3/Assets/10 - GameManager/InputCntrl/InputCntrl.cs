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
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 look = Vector2.zero;

        if (context.performed)
        {
            look = Mouse.current.position.ReadValue();
        } 
    }

    public void OnQuitEngagement(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //EventManager.Instance.InvokeOnQuitEngagment("Giving Up");
        }
    }

    /**
     * OnFire1() - 
     */
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
