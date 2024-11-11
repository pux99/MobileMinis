using System.Collections;
using System.Collections.Generic;
using ManagerScripts;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject winingUI;
    [SerializeField] private GameObject losingUI;

    [SerializeField] private DungeonManager dungeonManager;
    [SerializeField] private CountDown countDown;
    [SerializeField] private GameObject minigameSelector;

    void Start()
    {
        dungeonManager.WinTheCombat += WinCombatUI;
        dungeonManager.LossTheDungeon += LossUI;
        dungeonManager.WinDungeon += WinDungeonUI;
    }

    private void WinCombatUI()
    {
        NormalTurnOffs();
        winingUI.SetActive(true);
    }
    private void LossUI()
    {
        NormalTurnOffs();
        losingUI.SetActive(true);
    }
    private void WinDungeonUI()
    {
        NormalTurnOffs();
    }

    private void NormalTurnOffs()
    {
        minigameSelector.SetActive(false);
        countDown.TurnOffCountdown();
    }
    
}
