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
        //TODO: dang ton hieu nang k can thiet -> sua lai doan nay  (DONE)
        //targetListInRange.RemoveAll(Character => Character == null);
        //targetListInRange.RemoveAll(Character => Character.IsDead);

#if UNITY_EDITOR

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
#endif
    }

    public void OnInit()
    {
        enemyKilled = 0;
        isDead = false;
        targetListInRange.Clear();
        atkRange.ResetSize();
        ChangeWeapon.Instance.ChangeRangeWhenChangeWeapon(currentWeapon, this);
    }
    public void OnDespawn()
    {
        isDead = true;
        ChangeAnim(Constant.ANIM_DEATH);
        //SoundManager.Instance.PlaySFX(3);
        SoundManager.Instance.PlaySound(3);
    }
}
