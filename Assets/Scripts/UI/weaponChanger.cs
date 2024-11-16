using System.Collections;
using System.Collections.Generic;
using Minigames.Weapons;
using Player;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    [SerializeField]private SoPlayerStatsAndWeapons playerStatsAndWeapons;

    public void ChangeWeapon(Weapon newWeapon, SoPlayerStatsAndWeapons.WeaponType weaponType)
    {
        playerStatsAndWeapons.ChangeWeapon(newWeapon,weaponType);
    }

    
}
