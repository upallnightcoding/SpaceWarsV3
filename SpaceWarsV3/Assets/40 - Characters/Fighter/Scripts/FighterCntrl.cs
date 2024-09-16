using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterCntrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartTurn()
    {
        LeanTween.rotateAroundLocal(gameObject, Vector3.up, -360.0f, 10.0f).setLoopClamp();
    }

    public void FadeOut()
    {
        LeanTween.alpha(gameObject, 0.0f, 2.0f);
    }
}
