using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectCntrl : MonoBehaviour
{
    [SerializeField] private float speed    = 10.0f;
    [SerializeField] private float throttle = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObjectController(Vector3.zero);
    }

    private void ObjectController(Vector2 leftStick)
    {
        Debug.Log($"leftStick: {leftStick}/{Gamepad.current.leftStick.IsPressed()}");

        Vector3 direction = new Vector3();

        if (leftStick.magnitude > 0.1f && Gamepad.current.leftStick.IsPressed())
        {
            direction = new Vector3(leftStick.x, 0.0f, leftStick.y).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, 100.0f * Time.deltaTime);
            transform.localRotation = playerRotation;

            transform.Translate(transform.forward * speed * throttle * Time.deltaTime, Space.World);
        }

        if (Gamepad.current.leftStick.IsPressed())
        {
            transform.Translate(transform.forward * speed * throttle * Time.deltaTime, Space.World);
        }

    }

    private void OnEnable()
    {
        EventManager.Instance.OnInputMove += ObjectController;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnInputMove -= ObjectController;
    }
}
