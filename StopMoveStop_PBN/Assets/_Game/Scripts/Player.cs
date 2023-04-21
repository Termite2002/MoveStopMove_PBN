using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Character
{
    private Vector3 beginPos;
    public HeadPoint levelHeadPoint;
    public HeadPoint headpointPrefab;


    protected override void Start()
    {
        beginPos = TF.position;
        base.Start();
        isDead = false;
        currentWeapon = (WeaponType)Enum.Parse(typeof(WeaponType), SaveLoadController.Instance.currentWeapon, true);
        RenderWeaponToHold();
        ChangeWeapon.Instance.ChangeRangeWhenChangeWeapon(currentWeapon, this);
        LoadSkin();
    }

    public void OnInit()
    {
        TF.position = beginPos;
        enemyKilled = 0;
        isDead = false;
        targetListInRange.Clear();
        bodyScale.localScale = new Vector3(1f, 1f, 1f);
        AddHeadPoint();
        CameraFollow.Instance.ResetCam();
        ChangeWeapon.Instance.ChangeRangeWhenChangeWeapon(currentWeapon, this);
    }
    public void OnDespawn()
    {
        if (levelHeadPoint != null)
        {
            Destroy(levelHeadPoint.gameObject);
        }
        isDead = true;
        ChangeAnim(Constant.ANIM_DEATH);

        SoundManager.Instance.PlaySound(3);
    }
    public void LoadSkin()
    {
        Debug.Log("Load Skin");
        if (SaveLoadController.Instance.currentSkin != -1)
        {
            WeaponShopController.Instance.ChooseSkin(SaveLoadController.Instance.currentSkin);
        }
        else
        {
            if (SaveLoadController.Instance.currentHat != -1)
            {
                WeaponShopController.Instance.ChooseHatToWear(SaveLoadController.Instance.currentHat);
            }
            if (SaveLoadController.Instance.currentPant != -1)
            {
                WeaponShopController.Instance.ChoosePantToWear(SaveLoadController.Instance.currentPant);
            }
            if (SaveLoadController.Instance.currentShield != -1)
            {
                WeaponShopController.Instance.ChooseShieldToHold(SaveLoadController.Instance.currentShield);
            }
        }
    }
    public void AddHeadPoint()
    {
        levelHeadPoint = Instantiate(headpointPrefab); //ObjectPoolPro.Instance.GetFromPool(Constant.HEADPOINT);

        levelHeadPoint.SetOwner(this);
        levelHeadPoint.ChangePointText(enemyKilled);
        levelHeadPoint.gameObject.SetActive(true);
    }

}
