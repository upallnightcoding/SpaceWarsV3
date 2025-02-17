using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePortalCntrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit(Collider obstacle)
    {
        Debug.Log($"On Trigger Exit WavePortalCntrl: {obstacle.gameObject.name}");

        if (obstacle.CompareTag("Fighter"))
        {

        }
    }
}
