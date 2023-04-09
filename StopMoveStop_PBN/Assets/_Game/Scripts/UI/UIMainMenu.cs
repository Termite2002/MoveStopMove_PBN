using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UICanvas
{
    public Text coinText;
    private void OnEnable()
    {
        ChangeAnim("OpenMenu");
        coinText.text = SaveLoadController.Instance.gold.ToString();
    }
    public void PlayButton()
    {
        ChangeAnim("CloseMenu");
        UIManager.Instance.OpenUI<UIGameplay>();
        LevelManager.Instance.OnStart();
        Close(0.5f);
    }
    public void SettingButton()
    {
        UIManager.Instance.OpenUI<UISetting>();
    }
    public void ShopButton()
    {
        ChangeAnim("CloseMenu");
        UIManager.Instance.OpenUI<UIShop>();

        CameraFollow.Instance.StartZoomIn();
        WeaponShopController.Instance.BackToNormalSkin();
        Close(0.5f);
    }
    public void WeaponButton()
    {
        ChangeAnim("CloseMenu");
        UIManager.Instance.OpenUI<UIWeapon>();

        Close(0.5f);
    }

    
}
