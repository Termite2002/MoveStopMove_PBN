using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Data 
{
    public int gold;

    public int level_id;
    public int currentHat, currentPant, currentShield, currentSkin;

    public string currentWeapon = Constant.WEAPON_BOOMERANG;

    public List<int> weaponOwner = new List<int>();
    public List<int> hatOwner = new List<int>();
    public List<int> pantOwner = new List<int>();
    public List<int> shieldOwner = new List<int>();
    public List<int> skinOwner = new List<int>();
}
