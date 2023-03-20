using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveLoadController : Singleton<SaveLoadController>
{
    Player player;

    public int gold = 0;
    public string currentWeapon;
    void Awake()
    {
        Data data = SaveLoadManager.LoadData("savegame.dat") as Data;
        if (data != null)
        {
            gold = data.gold;
            if (data.currentWeapon != null)
            {
                currentWeapon = data.currentWeapon;
            }
        }
        else
        {
            gold = 0;
            currentWeapon = "Boomerang";
        }
        //Debug.Log(data.gold);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            player = FindObjectOfType<Player>();
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelManager.Instance.NextLevel();
        }
    }

    private void SaveData()
    {
        Data data = new Data();
        data.gold = gold;
        data.currentWeapon = player.currentWeapon.ToString();
        SaveLoadManager.SaveData(data, "savegame.dat");
        Debug.Log(data.gold);
    }
}
