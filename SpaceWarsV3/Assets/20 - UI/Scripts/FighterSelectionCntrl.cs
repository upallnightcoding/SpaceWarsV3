using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSelectionCntrl : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;

    private int fighter = 0;
    private GameObject currentFighter;

    public void NewGameBtn()
    {
        currentFighter = gameData.fighterList[fighter].Create(gameData.displayFighterCenter);
        currentFighter.GetComponent<FighterCntrl>().StartTurn(true);
    }

    public void FighterSelectionPrev()
    {
        Destroy(currentFighter);

        fighter = (--fighter < 0) ? gameData.fighterList.Length - 1 : fighter;

        currentFighter = gameData.fighterList[fighter].Create(gameData.displayFighterCenter);
        currentFighter.GetComponent<FighterCntrl>().StartTurn(true);
    }

    public void FighterSelectionNext()
    {
        Destroy(currentFighter);

        fighter = ++fighter % gameData.fighterList.Length;

        currentFighter = gameData.fighterList[fighter].Create(gameData.displayFighterCenter);
        currentFighter.GetComponent<FighterCntrl>().StartTurn(true);
    }

    public void FighterSelection()
    {
        Debug.Log("FighterSelection");
    }
}
