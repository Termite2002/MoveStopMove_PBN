using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UICanvas
{
    public Image soundOn, soundOff, vibrationOn, vibrationOff;
    private void OnEnable()
    {
        ChangeAnim(Constant.ANIM_OPEN_SETTING);
    }
    public void ContinueButton()
    {
        if (UIManager.Instance.IsOpened<UIGameplay>())
        {
            ChangeAnim(Constant.ANIM_CLOSE_SETTING);
            Close(0.5f);
            LevelManager.Instance.SetJoystickOn();
        }
        else
        {
            ChangeAnim(Constant.ANIM_CLOSE_SETTING);
            Close(0.5f);
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<UIMainMenu>();
        }
    }
    public void HomeButton()
    {
        if (UIManager.Instance.IsOpened<UIMainMenu>())
        {
            ChangeAnim(Constant.ANIM_CLOSE_SETTING);
            Close(0.5f);
        }
        else
        {
            LevelManager.Instance.DeleteLevel();
            ChangeAnim(Constant.ANIM_CLOSE_SETTING);
            Close(0.5f);
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<UIMainMenu>();
        }
        LevelManager.Instance.SetJoystickOff();
    }
    public void SoundButtonOn()
    {
        soundOn.enabled = true;
        soundOff.enabled = false;

        SoundManager.Instance.MuteSound();
    }
    public void SoundButtonOff()
    {
        soundOn.enabled = false;
        soundOff.enabled = true;

        SoundManager.Instance.TurnOnMusic();
    }
    public void VibraButtonOn()
    {
        vibrationOn.enabled = true;
        vibrationOff.enabled = false;

        SaveLoadController.Instance.vibrate = 0;
    }
    public void VibraButtonOff()
    {
        vibrationOn.enabled = false;
        vibrationOff.enabled = true;

        SaveLoadController.Instance.vibrate = 1;
    }
}
