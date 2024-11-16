using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCntrl : MonoBehaviour
{
    private Vector3 delta;
    private Transform fighter;
    private Vector3 movePosition;
    private Vector3 velocity = Vector3.zero;
    private float damping = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartEngagement(Transform fighter)
    {
        this.fighter = fighter;
        delta = fighter.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (fighter != null)
        {
            movePosition = fighter.position - delta;

            transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
        }
    }
}
