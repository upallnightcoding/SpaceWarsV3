using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private FighterSO fighter1;

    public void Start()
    {
        //fighter1.Create(new Vector3(2.6f, 0.0f, 7.7f));

        //fighter1.Create(gameData.displayFighterCenter);
        //NewGameAction();
    }

    public void NewGameAction()
    {
        Debug.Log("New Game Action ...");
        EventManager.Instance.InvokeOnNewGameAction();
    }

    public void LoadGameBtn()
    {

    }

    public void SettingsBtn()
    {

    }

    public void ScoreBoardBtn()
    {

    }

    public void QuitBtn()
    {

    }

    public class DisplayFighters
    {
        private FighterSO[] fighterList;

        private int fighter;

        public DisplayFighters(GameDataSO gameData)
        {
            this.fighterList = gameData.fighterList;
            this.fighter = fighterList.Length - 1;

            gameData.fighterList[fighter].Create(gameData.displayFighterCenter);
        }
    }
}


