using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCntrl : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;

    private bool displayFighters = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFighterDisplay());
    }

    public void StopFighterDisplay()
    {
        displayFighters = false;
    }

    private IEnumerator StartFighterDisplay()
    {
        float rotation = 90.0f;
        while(displayFighters)
        {
            int display = Random.Range(0, gameData.fighterList.Length);
            GameObject go = gameData.fighterList[display].Create(gameData.displayFighterCenter);
            go.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            go.GetComponent<FighterCntrl>().StartTurn();
            yield return new WaitForSeconds(3.0f);
            //go.GetComponent<FighterCntrl>().FadeOut();
            //yield return new WaitForSeconds(3.0f);
            Destroy(go);

            rotation = go.transform.rotation.eulerAngles.y;
        }
    }
}
