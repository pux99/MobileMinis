using System.Collections;
using System.Collections.Generic;
using Core;
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
    [SerializeField] private GameObject enemy;
    

    void Start()
    {
        ServiceLocator.Instance.GetService<EventManager>().CombatWin += WinCombatUI;
        ServiceLocator.Instance.GetService<EventManager>().CombatLoss += LossUI;
        ServiceLocator.Instance.GetService<EventManager>().CombatStart += CombatUITurnOn;

        dungeonManager.WinDungeon += WinDungeonUI;
    }

    private void CombatUITurnOn()
    {
        countDown.gameObject.SetActive(true);
        enemy.SetActive(true);
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
