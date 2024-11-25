using System.Collections;
using System.Collections.Generic;
using Core;
using HealthSystem;
using ManagerScripts;
using UnityEngine;

public class TargetGiver : MonoBehaviour
{
    public enum Target
    {
        Player,
        Enemy
    }

    [SerializeField] private UHealth player;
    [SerializeField] private UHealth enemy;
    
    private void Awake()
    {
        ServiceLocator serviceLocator = ServiceLocator.Instance;
        serviceLocator.RegisterService<TargetGiver>(this);
    }

    

    public UHealth GetTarget(Target target)
    {
        switch (target)
        {
            case Target.Player:
                return player;
            case Target.Enemy:
                return enemy;
            default:
                return null;
        }
    }
}
