using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterCntrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartTurn(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTurn(bool turning)
    {
        LeanTween.rotateAroundLocal(gameObject, Vector3.up, -360.0f, 10.0f).setLoopClamp();
    }
}
