using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UICanvas
{
    Player player;
    public Image soundOn, soundOff, vibrationOn, vibrationOff;
    private void OnEnable()
    {
        //player = FindObjectOfType<Player>();
        //player.GetComponent<PlayerController>().enabled = false;
        ChangeAnim("OpenSetting");
    }
    public void ContinueButton()
    {
        ChangeAnim("CloseSetting");
        Close(0.5f);
    }
    public void HomeButton()
    {
        if (UIManager.Instance.IsOpened<UIMainMenu>())
        {
            ChangeAnim("CloseSetting");
            Close(0.5f);
        }
        else
        {
            ChangeAnim("CloseSetting");
            Close(0.5f);
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
