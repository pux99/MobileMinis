using System.Collections;
using System.Collections.Generic;
using Core;
using ManagerScripts;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class UiManager : MonoBehaviour
{
    [FormerlySerializedAs("winingUI")] [SerializeField] private GameObject winingDungeonUI;
    [SerializeField] private GameObject winingCombatUI;
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
        ServiceLocator.Instance.GetService<EventManager>().DungeonWin += WinDungeonUI;

        dungeonManager.WinDungeon += WinDungeonUI;
    }

    private void CombatUITurnOn()
    {
        countDown.gameObject.SetActive(true);
        enemy.SetActive(true);
        minigameSelector.SetActive(true);
    }

    private void WinCombatUI()
    {
        winingCombatUI.SetActive(true);
        enemy.SetActive(false);
    }
    private void LossUI()
    {
        losingUI.SetActive(true);
    }
    private void WinDungeonUI()
    {
        winingDungeonUI.SetActive(true);
    }
    
    
}
