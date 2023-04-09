using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWin : UICanvas
{
    public Text coinGain;
    private void OnEnable()
    {
        coinGain.text = LevelManager.Instance.coinGainInLevel.ToString();
    }
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
