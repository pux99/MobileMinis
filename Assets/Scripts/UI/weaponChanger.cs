using System;
using System.Collections;
using System.Collections.Generic;
using Minigames.Weapons;
using Player;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    [SerializeField]private SoPlayerStatsAndWeapons playerStatsAndWeapons;

    private void Start()
    {
        playerStatsAndWeapons = FindObjectOfType<SoPlayerStatsAndWeapons>();
    }

    public void ChangeWeapon(Weapon newWeapon, SoPlayerStatsAndWeapons.WeaponType weaponType)
    {
        playerStatsAndWeapons.ChangeWeapon(newWeapon,weaponType);
    }

    
}
