using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattlePanelCntrl : MonoBehaviour
{
    const float WAIT_PERIOD = 1.0f;

    [SerializeField] private TMP_Text secondsText;

    private int counter;

    private bool startTimerSw;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTimer()
    {
        secondsText.text = "0000";
        counter = 0;
        startTimerSw = true;
        StartCoroutine(StartTimerRoutine());
    }

    private void EndTimer(string message)
    {
        startTimerSw = false;
    }

    private IEnumerator StartTimerRoutine()
    {
        while (startTimerSw)
        {
            yield return new WaitForSecondsRealtime(WAIT_PERIOD);
            secondsText.text = (++counter).ToString("D4");
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.OnQuitEngagment += EndTimer;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnQuitEngagment -= EndTimer;
    }
}