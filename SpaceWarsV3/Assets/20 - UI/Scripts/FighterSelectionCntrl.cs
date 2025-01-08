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

        int index = --fighter % gameData.fighterList.Length;

        Debug.Log($"Fighter Index: {index}/{fighter}");

        currentFighter = gameData.fighterList[index].Create(gameData.fighterSelectionCenter);
        currentFighter.GetComponent<FighterCntrl>().StartTurn();
    }

    public void FighterSelectionNext()
    {
        Destroy(currentFighter);

        int index = ++fighter % gameData.fighterList.Length;

        Debug.Log($"Fighter Index: {index}/{fighter}");

        currentFighter = gameData.fighterList[index].Create(gameData.fighterSelectionCenter);
        currentFighter.GetComponent<FighterCntrl>().StartTurn();
    }

    public void FighterSelection()
    {
        currentFighter.SetActive(false);
        EventManager.Instance.InvokeOnFighterSelection(currentFighter);
    }
}
