using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UICanvas
{
    public Text coinText;
    private void OnEnable()
    {
        ChangeAnim(Constant.ANIM_OPEN_MENU);
        coinText.text = SaveLoadController.Instance.gold.ToString();
    }
    public void PlayButton()
    {
        ChangeAnim(Constant.ANIM_CLOSE_MENU);
        UIManager.Instance.OpenUI<UIGameplay>();
        LevelManager.Instance.OnStart();
        Close(0.5f);
    }
    public void SettingButton()
    {
        ChangeAnim(Constant.ANIM_CLOSE_MENU);
        UIManager.Instance.OpenUI<UISetting>();
        Close(0.5f);
    }
    public void ShopButton()
    {
        ChangeAnim(Constant.ANIM_CLOSE_MENU);
        UIManager.Instance.OpenUI<UIShop>();

        CameraFollow.Instance.StartZoomIn();
        WeaponShopController.Instance.BackToNormalSkin();
        Close(0.5f);
    }
    public void WeaponButton()
    {
        ChangeAnim(Constant.ANIM_CLOSE_MENU);
        UIManager.Instance.OpenUI<UIWeapon>();

        Close(0.5f);
    }

    
}
