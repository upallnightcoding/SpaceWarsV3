using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPortalCntrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit(Collider obstacle)
    {
        Debug.Log($"Spin Portal  On Trigger Exit ... {obstacle.tag}");

        if (obstacle.CompareTag("Fighter"))
        {
            Debug.Log("Spin Portal Trigger ...");
        }
    }
}
