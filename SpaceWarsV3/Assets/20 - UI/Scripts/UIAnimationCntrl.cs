using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationCntrl : MonoBehaviour
{
    [Header("Animations ...")]
    [SerializeField] private GameObject hud_Crosshair;

    // Start is called before the first frame update
    void Start()
    {
        //LeanTween.rotateAround(hud_Crosshair, Vector3.forward, 360.0f, 2.0f).setLoopClamp();
    }
}
