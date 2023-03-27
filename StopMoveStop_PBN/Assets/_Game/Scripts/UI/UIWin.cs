using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWin : UICanvas
{
    public void HomeButton()
    {
        LevelManager.Instance.DeleteLevel();
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<UIMainMenu>();
    }
    public void NextLevelButton()
    {
        CloseDirectly();
        LevelManager.Instance.NextLevel();
    }
}
