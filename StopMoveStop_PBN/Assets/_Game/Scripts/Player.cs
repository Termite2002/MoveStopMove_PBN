using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Character
{

    protected override void Start()
    {
        base.Start();
        isDead = false;
        currentWeapon = (WeaponType)Enum.Parse(typeof(WeaponType), SaveLoadController.Instance.currentWeapon, true);
        RenderWeaponToHold();
        ChangeWeapon.Instance.ChangeRangeWhenChangeWeapon(currentWeapon, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            ChangeAnim("Dead");
        }

        targetListInRange.RemoveAll(Character => Character == null);
        targetListInRange.RemoveAll(Character => Character.GetComponent<Character>().isDead);

        // test
        if (Input.GetKeyDown(KeyCode.B))
        {
            currentWeapon = WeaponType.Boomerang;
            ChangeWeapon.Instance.ChangeRangeWhenChangeWeapon(currentWeapon, this);
            RenderWeaponToHold();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentWeapon = WeaponType.Axe;
            ChangeWeapon.Instance.ChangeRangeWhenChangeWeapon(currentWeapon, this);
            RenderWeaponToHold();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentWeapon = WeaponType.Sword;
            ChangeWeapon.Instance.ChangeRangeWhenChangeWeapon(currentWeapon, this);
            RenderWeaponToHold();
        }
    }

    public void OnInit()
    {
        enemyKilled = 0;
        isDead = false;
        targetListInRange.Clear();
        ChangeWeapon.Instance.ChangeRangeWhenChangeWeapon(currentWeapon, this);
    }

}
