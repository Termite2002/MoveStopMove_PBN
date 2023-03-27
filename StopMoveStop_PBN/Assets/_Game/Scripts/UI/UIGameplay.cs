using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplay : UICanvas
{
    public void SettingButton()
    {
        UIManager.Instance.OpenUI<UISetting>();
    }
}
