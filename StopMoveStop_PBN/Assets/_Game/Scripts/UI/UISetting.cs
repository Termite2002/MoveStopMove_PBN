using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UICanvas
{
    public Image soundOn, soundOff, vibrationOn, vibrationOff;
    public void ContinueButton()
    {
        CloseDirectly();
    }
    public void HomeButton()
    {
        if (UIManager.Instance.IsOpened<UIMainMenu>())
        {
            CloseDirectly();
        }
        else
        {
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<UIMainMenu>();
        }
    }
    public void SoundButtonOn()
    {
        soundOn.enabled = true;
        soundOff.enabled = false;
    }
    public void SoundButtonOff()
    {
        soundOn.enabled = false;
        soundOff.enabled = true;
    }
    public void VibraButtonOn()
    {
        vibrationOn.enabled = true;
        vibrationOff.enabled = false;
    }
    public void VibraButtonOff()
    {
        vibrationOn.enabled = false;
        vibrationOff.enabled = true;
    }
}
