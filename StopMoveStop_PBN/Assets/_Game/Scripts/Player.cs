using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Character
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        isDead = false;
        currentWeapon = (WeaponType)Enum.Parse(typeof(WeaponType), SaveLoadController.Instance.currentWeapon, true); 
    }

    // Update is called once per frame
    void Update()
    {
        targetListInRange.RemoveAll(Character => Character == null);
        targetListInRange.RemoveAll(Character => Character.GetComponent<Character>().isDead);

        if (Input.GetKeyDown(KeyCode.B))
        {
            currentWeapon = WeaponType.Boomerang;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentWeapon = WeaponType.Axe;
        }
    }

    public void OnInit()
    {
        isDead = false;
        targetListInRange.Clear();
    }
}
