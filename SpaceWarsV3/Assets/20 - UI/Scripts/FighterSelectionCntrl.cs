using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterSelectionCntrl : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;

    private int fighter = 0;
    private GameObject currentFighter;

    public void NewGameAction()
    {
        currentFighter = gameData.fighterList[fighter].Create(gameData.mainMenuFighterCenter);
        currentFighter.GetComponent<FighterCntrl>().StartTurn();
    }

    public void FighterSelectionPrev()
    {
        Destroy(currentFighter);

        //fighter = (--fighter < 0) ? gameData.fighterList.Length - 1 : fighter;
        fighter = --fighter % gameData.fighterList.Length;

        currentFighter = gameData.fighterList[fighter].Create(gameData.fighterSelectionCenter);
        currentFighter.GetComponent<FighterCntrl>().StartTurn();
    }

    public void FighterSelectionNext()
    {
        Destroy(currentFighter);

        fighter = ++fighter % gameData.fighterList.Length;

        currentFighter = gameData.fighterList[fighter].Create(gameData.fighterSelectionCenter);
        currentFighter.GetComponent<FighterCntrl>().StartTurn();
    }

    public void FighterSelection()
    {
        currentFighter.SetActive(false);
        EventManager.Instance.InvokeOnFighterSelection(currentFighter);
    }
}
