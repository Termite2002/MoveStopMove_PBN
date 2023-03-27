using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    public void PlayButton()
    {
        UIManager.Instance.OpenUI<UIGameplay>();
        LevelManager.Instance.OnStart();
        CloseDirectly();
    }
    public void SettingButton()
    {
        UIManager.Instance.OpenUI<UISetting>();
    }
}
