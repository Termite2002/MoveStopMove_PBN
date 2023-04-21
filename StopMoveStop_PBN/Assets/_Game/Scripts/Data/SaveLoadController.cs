using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveLoadController : Singleton<SaveLoadController>
{
    Player player;

    public int vibrate;
    public int gold = 0;
    public int currentHat, currentPant, currentShield, currentSkin;

    public string currentWeapon;
    public List<int> weaponOwner;
    public List<int> hatOwner;
    public List<int> pantOwner;
    public List<int> shieldOwner;
    public List<int> skinOwner;
    void Awake()
    {
        Data data = SaveLoadManager.LoadData("savegame.dat") as Data;
        if (data != null)
        {
            vibrate = 1;
            // Owner
            weaponOwner = data.weaponOwner;
            hatOwner = data.hatOwner;
            pantOwner = data.pantOwner;
            shieldOwner = data.shieldOwner;
            skinOwner = data.skinOwner;

            gold = data.gold;

            // current
            currentHat = data.currentHat;
            currentPant = data.currentPant;
            currentShield = data.currentShield;
            currentSkin = data.currentSkin;
            if (data.currentWeapon != null)
            {
                currentWeapon = data.currentWeapon;
            }
        }
        else
        {

            vibrate = 1;
            gold = 1000;
            currentHat = currentPant = currentShield = currentSkin = -1;
            currentWeapon = Constant.WEAPON_BOOMERANG;

        }
        //Debug.Log(data.gold);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            player = FindObjectOfType<Player>();
            gold += 999;
            SaveData(player);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelManager.Instance.NextLevel();
            //Debug.Log(currentHat);
        }
    }

    public void SaveData(Player player)
    {
        Data data = new Data();
        data.gold = gold;
        data.weaponOwner = weaponOwner;
        data.pantOwner = pantOwner;
        data.hatOwner = hatOwner;
        data.shieldOwner = shieldOwner;
        data.skinOwner = skinOwner;

        data.currentHat = currentHat;
        data.currentPant = currentPant;
        data.currentShield = currentShield;
        data.currentSkin = currentSkin;
        data.currentWeapon = player.currentWeapon.ToString();

        SaveLoadManager.SaveData(data, "savegame.dat");
        Debug.Log(data.vibrate);
    }
}
