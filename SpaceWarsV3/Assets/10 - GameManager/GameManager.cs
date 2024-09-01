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
        NewGameBtn();
    }

    public void NewGameBtn()
    {
       
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
}
