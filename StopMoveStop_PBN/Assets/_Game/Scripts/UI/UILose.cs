using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILose : UICanvas
{ 
    public void HomeButton()
    {
        LevelManager.Instance.DeleteLevel();
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<UIMainMenu>();
    }
    public void RetryButton()
    {
        CloseDirectly();
        LevelManager.Instance.LoadLevel();
    }
}
