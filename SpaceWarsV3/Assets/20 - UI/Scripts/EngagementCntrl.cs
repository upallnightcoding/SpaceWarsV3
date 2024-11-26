using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EngagementCntrl : MonoBehaviour
{
    [SerializeField] private TMP_Text countDownText;

    public void StartEngagementCountDown(LevelData levelData)
    {
        StartCoroutine(StartFighterEngagement(levelData));
    }

    private IEnumerator StartFighterEngagement(LevelData levelData)
    {
        float transitionTime = 6.0f;

        yield return null;

        while (transitionTime > 0.0f)
        {
            DisplayEngageCountDown((int)transitionTime);

            yield return null;

            transitionTime -= Time.deltaTime;
        }

        EventManager.Instance.InvokeOnStartBattle(levelData);

    }

    private void DisplayEngageCountDown(int count)
    {
        if (count != 0)
        {
            countDownText.fontSize = 82;
            countDownText.text = count.ToString();
        }
        else
        {
            countDownText.fontSize = 38;
            countDownText.text = "Engage";
        }
    }
}
