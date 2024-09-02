using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICntrl : MonoBehaviour
{
    [SerializeField] FighterSelectionCntrl fighterSelectionCntrl;

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject fighterSelectionPanel;

   public void NewGameAction()
    {
        CloseAllPanels();
        fighterSelectionPanel.SetActive(true);

        fighterSelectionCntrl.NewGameAction();
    }

    private void FighterSelection()
    {

    }

    private void CloseAllPanels()
    {
        mainMenuPanel.SetActive(false);
        fighterSelectionPanel.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.Instance.OnNewGameAction -= NewGameAction;
    }

    private void OnEnable()
    {
        EventManager.Instance.OnNewGameAction += NewGameAction;
    }
}
