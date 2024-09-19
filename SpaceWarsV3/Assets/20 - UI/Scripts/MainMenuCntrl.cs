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

    /**
     * StopFighterDisplay() - Stops the display of fighters.
     */
    public void StopFighterDisplay() => displayFighters = false;

    /**
     * StartFighterDisplay() - Select a random fighter and turn it on it's y
     * axis. The turning will continue for WaitForSeconds() and then terminate.
     * The display of the next fighter will continue where the first left
     * off.  The loop can also be terminated when the displayFighter variable
     * is set to false.
     */
    private IEnumerator StartFighterDisplay()
    {
        float rotation = 0.0f;

        while(displayFighters)
        {
            int fighter = Random.Range(0, gameData.fighterList.Length);
            GameObject go = gameData.fighterList[fighter].Create(gameData.displayFighterCenter);
            go.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            go.GetComponent<FighterCntrl>().StartTurn();
            yield return new WaitForSeconds(3.0f);
            rotation = go.transform.rotation.eulerAngles.y;

            Destroy(go);
        }
    }
}
