using Enemies;
using HealthSystem;
using ManagerScripts;
using UI;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private CountDown countDown;
    [SerializeField] private EnemyUIManager enemyUIManager;
    [SerializeField] private UHealth health;
    [SerializeField] private SoEnemy enemy;
    [SerializeField] private BattleManager battleManager;
    public UHealth Health => health;
    void Start()
    {
        health.OnDead += Defeated;
        health.OnLifeChange += HandleLifeChange;
        countDown.CountdownEnd += Attack;
        SetUpEnemy(enemy);
    }


    public void SetUpEnemy(SoEnemy newEnemy)
    {
        enemy = newEnemy;
        countDown.SetAttackCooldown(enemy.attackFrequency);
        health.SetMaxHp(enemy.maxLife);
        health.FullHeal();
        enemyUIManager.SetUpArt(enemy.enemyArt);
        
    }

    private void Attack()
    {
        enemy.Attack(battleManager.PlayerCombatManager.PlayerHealth);
        enemyUIManager.PlayAttackAnimation();
    }
    private void HandleLifeChange(int arg1, int arg2)
    {
        enemyUIManager.PlayDamagedAnimation();
    }

    private void Defeated()
    {
        battleManager.EnemyDefeated();
    }
}
